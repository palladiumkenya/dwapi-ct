using System;
using System.Configuration;
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
    public class SyncPatientStatusExtractCommandTests
    {
        private DwapiRemoteContext _context;
        private ISyncPatientStatusExtractCommand _syncCommand;
        private IProgress<DProgress> _dprogress;
        private IEMRRepository _emrRepository;

        [SetUp]
        public void SetUp()
        {
            _dprogress = new Progress<DProgress>(ReportDProgress);
            _context = new DwapiRemoteContext();
            _emrRepository = new EMRRepository(_context);
            _context.Database.ExecuteSqlCommand("DELETE FROM EventHistory;DELETE FROM PatientStatusExtract;DELETE FROM TempPatientStatusExtract;");
            _context.Database.ExecuteSqlCommand("DELETE FROM PatientExtract;DELETE FROM TempPatientExtract;DELETE FROM ValidationError;");
        }

        [Test]
        public void should_sync_patients()
        {
            var clearExtractsCommand = new ClearExtractsCommand(_emrRepository);
            var clearExtracts = clearExtractsCommand.ExecuteAsync().Result;
            var analyzeTempExtractsCommand = new AnalyzeTempExtractsCommand(_emrRepository, new DatabaseManager(_context));
            var eventHistories = analyzeTempExtractsCommand.ExecuteAsync().Result;

            var pextractCommand = new LoadPatientExtractCommand(_emrRepository);
            var pextracts = pextractCommand.ExecuteAsync().Result;
            var pvalidateCommand = new ValidatePatientExtractCommand(_emrRepository, new ValidatorRepository(_context));
            var pvalidations = pvalidateCommand.ExecuteAsync().Result;
            var psyncCommand = new SyncPatientExtractCommand(_emrRepository);
            var ptotals = psyncCommand.ExecuteAsync().Result;

            var extractCommand = new LoadPatientStatusExtractCommand(_emrRepository);
            var extracts = extractCommand.ExecuteAsync().Result;
            var validateCommand = new ValidatePatientStatusExtractCommand(_emrRepository, new ValidatorRepository(_context));
            var validations = validateCommand.ExecuteAsync().Result;

            var watch = System.Diagnostics.Stopwatch.StartNew();
            _syncCommand = new SyncPatientStatusExtractCommand(_emrRepository);
            var totals = _syncCommand.ExecuteAsync(_dprogress).Result;

            watch.Stop();
            var tempRecords = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM PatientStatusExtract")
                .Single();
            Assert.IsTrue(tempRecords > 0);

            var emr = _emrRepository.GetDefault();
            var extractSettingId = emr.GetActiveExtractSetting("TempPatientStatusExtract").Id;
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
            _context.Database.ExecuteSqlCommand("DELETE FROM EventHistory;DELETE FROM PatientStatusExtract;DELETE FROM TempPatientStatusExtract;");
            _context.Database.ExecuteSqlCommand("DELETE FROM PatientExtract;DELETE FROM TempPatientExtract;DELETE FROM ValidationError;");
        }

        private void ReportDProgress(DProgress value)
        {
            Console.WriteLine(value);
        }
    }
}
