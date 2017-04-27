using System;
using System.Configuration;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Command;
using PalladiumDwh.ClientReader.Infrastructure.Data.Repository;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data.Command
{
    public class SyncPatientVisitExtractCommandTests
    {
        private readonly string _connectionString =ConfigurationManager.ConnectionStrings["DWAPIRemote"].ConnectionString;

        private DwapiRemoteContext _context;
        private ISyncPatientVisitExtractCommand _syncCommand;
        private int top = 10;

        [SetUp]
        public void SetUp()
        {
            _context=new DwapiRemoteContext();
            _context.Database.ExecuteSqlCommand("DELETE FROM PatientVisitExtract;DELETE FROM TempPatientVisitExtract");

            _context.Database.ExecuteSqlCommand("DELETE FROM PatientExtract;DELETE FROM TempPatientExtract");
            var extractCommand = new LoadPatientExtractCommand(new EMRRepository(_context));
            extractCommand.Execute();
            var syncPatientsCommand = new SyncPatientExtractCommand(new EMRRepository(_context));
            syncPatientsCommand.Execute();
        }

        [Test]
        public void should_sync_patients()
        {
            var extractCommand = new LoadPatientVisitExtractCommand(new EMRRepository(_context));
            extractCommand.Execute();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            _syncCommand = new SyncPatientVisitExtractCommand(new EMRRepository(_context));
            _syncCommand.Execute();

            watch.Stop();
            var tempRecords = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM PatientVisitExtract")
                .Single();
            Assert.IsTrue(tempRecords>0);
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Loaded {tempRecords} Temp records! in {elapsedMs}ms ({elapsedMs / 1000}s)");
        }

  
        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM PatientVisitExtract;DELETE FROM TempPatientVisitExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM PatientExtract;DELETE FROM TempPatientExtract");
        }
    }
}
