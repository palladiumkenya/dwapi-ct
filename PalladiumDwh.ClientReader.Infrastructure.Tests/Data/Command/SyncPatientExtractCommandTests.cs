using System;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Command;
using PalladiumDwh.ClientReader.Infrastructure.Data.Repository;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data.Command
{
    public class SyncPatientExtractCommandTests
    {
        private DwapiRemoteContext _context;
        private ISyncPatientExtractCommand _syncCommand;
        private IProgress<DProgress> _dprogress;
        private IEMRRepository _emrRepository;

        [SetUp]
        public void SetUp()
        {
            _dprogress = new Progress<DProgress>(ReportDProgress);
            _context =new DwapiRemoteContext();
            _emrRepository = new EMRRepository(_context);
            _context.Database.ExecuteSqlCommand("DELETE FROM EventHistory;DELETE FROM PatientExtract;DELETE FROM TempPatientExtract;DELETE FROM ValidationError;");
        }

        [Test]
        public void should_sync_patients()
        {
            var clearExtractsCommand = new ClearExtractsCommand(_emrRepository);
            var analyzeTempExtractsCommand = new AnalyzeTempExtractsCommand(_emrRepository, new DatabaseManager(_context));
            var result = clearExtractsCommand.ExecuteAsync().Result;
            var eventHistories = analyzeTempExtractsCommand.ExecuteAsync().Result;
            var extractCommand = new LoadPatientExtractCommand(new EMRRepository(_context));
            var extracts = extractCommand.ExecuteAsync().Result;
            var validateCommand = new ValidatePatientExtractCommand(_emrRepository, new ValidatorRepository(_context));
            var validations = validateCommand.ExecuteAsync().Result;

            var watch = System.Diagnostics.Stopwatch.StartNew();
            _syncCommand = new SyncPatientExtractCommand(new EMRRepository(_context));
            var totals= _syncCommand.ExecuteAsync(_dprogress).Result;

            watch.Stop();
            var tempRecords = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM PatientExtract")
                .Single();
            Assert.IsTrue(tempRecords>0);


            var emr = _emrRepository.GetDefault();
            var extractSettingId = emr.GetActiveExtractSetting("TempPatientExtract").Id;
            var eventsHistory = _emrRepository.GetStats(extractSettingId);
            Assert.AreEqual(tempRecords, eventsHistory.Imported);
            Console.WriteLine(eventsHistory.ImportedInfo());
            Console.WriteLine(eventsHistory.NotImportedInfo());

            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(_syncCommand.Summary);
            Console.WriteLine($"Loaded {tempRecords} Temp records! in {elapsedMs}ms ({elapsedMs / 1000}s)");

        }

  
        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM EventHistory;DELETE FROM PatientExtract;DELETE FROM TempPatientExtract;DELETE FROM ValidationError;");
        }

        private void ReportDProgress(DProgress value)
        {
            Console.WriteLine(value);
        }
    }
}
