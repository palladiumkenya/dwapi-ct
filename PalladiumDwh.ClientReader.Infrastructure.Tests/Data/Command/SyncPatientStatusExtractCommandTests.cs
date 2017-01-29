using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Command;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data.Command
{
    public class SyncPatientStatusExtractCommandTests
    {
        private readonly string _connectionString =ConfigurationManager.ConnectionStrings["DWAPIRemote"].ConnectionString;
        private readonly string _srcConnectionString = ConfigurationManager.ConnectionStrings["EMRDatabase"].ConnectionString;

        private DwapiRemoteContext _context;
        private ISyncPatientStatusExtractCommand _syncCommand;
        private int top = 10;

        [SetUp]
        public void SetUp()
        {
            _context=new DwapiRemoteContext();
            _context.Database.ExecuteSqlCommand("DELETE FROM PatientStatusExtract;DELETE FROM TempPatientStatusExtract");

            _context.Database.ExecuteSqlCommand("DELETE FROM PatientExtract;DELETE FROM TempPatientExtract");
            var extractCommand = new LoadPatientExtractDbCommand(new SqlConnection(_srcConnectionString), new SqlConnection(_connectionString), TestHelpers.GetPatientsSql(top));
            extractCommand.Execute();
            var syncPatientsCommand = new SyncPatientExtractDbCommand(_connectionString);
            syncPatientsCommand.Execute();
        }

        [Test]
        public void should_sync_patients()
        {
            var extractCommand = new LoadPatientStatusExtractDbCommand(new SqlConnection(_srcConnectionString), new SqlConnection(_connectionString), TestHelpers.GetPatientStatusSql(top));
            extractCommand.Execute();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            _syncCommand = new SyncPatientStatusExtractDbCommand(_connectionString);
            _syncCommand.Execute();

            watch.Stop();
            var tempRecords = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM PatientStatusExtract")
                .Single();
            Assert.IsTrue(tempRecords>0);
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Loaded {tempRecords} Temp records! in {elapsedMs}ms ({elapsedMs / 1000}s)");
        }

  
        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM PatientStatusExtract;DELETE FROM TempPatientStatusExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM PatientExtract;DELETE FROM TempPatientExtract");
        }
    }
}
