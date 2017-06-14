using System;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Command;
using PalladiumDwh.ClientReader.Infrastructure.Data.Repository;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data.Command
{
    public class ValidatePatientVisitExtractDbCommandTests
    {
        private DwapiRemoteContext _context;
        private IValidatePatientVisitExtractCommand _extractCommand;
        private IEMRRepository _emrRepository;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext();
            _emrRepository = new EMRRepository(_context);
            _extractCommand = new ValidatePatientVisitExtractCommand(_emrRepository, new ValidatorRepository(_context));
            _context.Database.ExecuteSqlCommand("DELETE FROM EventHistory;DELETE FROM TempPatientVisitExtract;DELETE FROM ValidationError");
        }

        [Test]
        public void should_Execute_Validate_PatientVisitExtract_DbCommand()
        {
            var clearExtractsCommand = new ClearExtractsCommand(_emrRepository);
            var analyzeTempExtractsCommand = new AnalyzeTempExtractsCommand(_emrRepository, new DatabaseManager(_context));
            var result2 = clearExtractsCommand.ExecuteAsync().Result;
            var eventHistories = analyzeTempExtractsCommand.ExecuteAsync().Result;

            var result = new LoadPatientVisitExtractCommand(_emrRepository).ExecuteAsync().Result;
            _context.Database.ExecuteSqlCommand("UPDATE TempPatientVisitExtract SET SiteCode=NULL;");

            var watch = System.Diagnostics.Stopwatch.StartNew();

            var summary = _extractCommand.ExecuteAsync().Result;
            watch.Stop();
            var errorRecords = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM ValidationError")
                .Single();

            var records = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM TempPatientVisitExtract WHERE CheckError=1")
                .Single();

            Assert.IsTrue(records > 0);
            Assert.IsTrue(errorRecords > 0);
            Assert.AreEqual(records, summary.Total);

            var emr = _emrRepository.GetDefault();
            var extractSettingId = emr.GetActiveExtractSetting("TempPatientVisitExtract").Id;

            var eventsHistory = _emrRepository.GetStats(extractSettingId);

            Assert.AreEqual(records, eventsHistory.Rejected);
            Console.WriteLine(eventsHistory.RejectedInfo());
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Validated {records} records! in {elapsedMs}ms ({elapsedMs / 1000}s)");
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM EventHistory;DELETE FROM TempPatientVisitExtract;DELETE FROM ValidationError");
            _context.SaveChanges();
        }
    }
}
