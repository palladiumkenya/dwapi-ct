﻿using System;
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
    public class LoadPatientBaselinesExtractDbCommandTests
    {
        private DwapiRemoteContext _context;
        private ILoadPatientBaselinesExtractCommand _extractCommand;
        private IEMRRepository _emrRepository;
        private IProgress<DProgress> _dprogress;

        [SetUp]
        public void SetUp()
        {
            _dprogress = new Progress<DProgress>(ReportDProgress);


            _context = new DwapiRemoteContext();
            _emrRepository = new EMRRepository(_context);

            _extractCommand = new LoadPatientBaselinesExtractCommand(_emrRepository);
            _context.Database.ExecuteSqlCommand("DELETE FROM EventHistory");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientBaselinesExtract");
        }

        [Test]
        public void should_Execute_Load_PatientBaselinesExtract_DbCommand()
        {
            var clearExtractsCommand = new ClearExtractsCommand(_emrRepository);
            var analyzeTempExtractsCommand = new AnalyzeTempExtractsCommand(_emrRepository, new DatabaseManager(_context));
            var result = clearExtractsCommand.ExecuteAsync().Result;
            var eventHistories = analyzeTempExtractsCommand.ExecuteAsync().Result;

            var watch = System.Diagnostics.Stopwatch.StartNew();
            var summary = _extractCommand.ExecuteAsync(null, _dprogress).Result;
            watch.Stop();
            var records = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM TempPatientBaselinesExtract")
                .Single();

            Assert.IsTrue(records == summary.Loaded);

            Console.WriteLine($"Summary:{summary}");
            Console.WriteLine($"Summary Error:{summary.ErrorStatus()}");

            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Loaded {records} records! in {elapsedMs}ms ({elapsedMs / 1000}s)");
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM EventHistory");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientBaselinesExtract");
            _context.SaveChanges();
        }

        private void ReportDProgress(DProgress value)
        {
            Console.WriteLine(value);
        }
    }
}
