using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Command;
using PalladiumDwh.ClientReader.Infrastructure.Data.Repository;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data.Command
{
    public class SyncPatientLaboratoryExtractCommandTests
    {
        private readonly string _connectionString =ConfigurationManager.ConnectionStrings["DWAPIRemote"].ConnectionString;

        private DwapiRemoteContext _context;
        private ISyncPatientLaboratoryExtractCommand _syncCommand;
        private int top = 10;

        [SetUp]
        public void SetUp()
        {
            _context=new DwapiRemoteContext();
            _context.Database.ExecuteSqlCommand("DELETE FROM PatientLaboratoryExtract;DELETE FROM TempPatientLaboratoryExtract");

            _context.Database.ExecuteSqlCommand("DELETE FROM PatientExtract;DELETE FROM TempPatientExtract");
            var extractCommand = new LoadPatientExtractDbCommand(new EMRRepository(_context));
            extractCommand.Execute();
            var syncPatientsCommand = new SyncPatientExtractDbCommand(_connectionString);
            syncPatientsCommand.Execute();
        }

        [Test]
        public void should_sync_patients()
        {
            var extractCommand = new LoadPatientLaboratoryExtractDbCommand(new EMRRepository(_context));
            extractCommand.Execute();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            _syncCommand = new SyncPatientLaboratoryExtractDbCommand(_connectionString);
            _syncCommand.Execute();

            watch.Stop();
            var tempRecords = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM PatientLaboratoryExtract")
                .Single();
            Assert.IsTrue(tempRecords>0);
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Loaded {tempRecords} Temp records! in {elapsedMs}ms ({elapsedMs / 1000}s)");
        }

  
        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM PatientLaboratoryExtract;DELETE FROM TempPatientLaboratoryExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM PatientExtract;DELETE FROM TempPatientExtract");
        }
    }
}
