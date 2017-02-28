using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Command;
using PalladiumDwh.ClientReader.Infrastructure.Data.Repository;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data.Command
{
    public class LoadPatientVisitExtractDbCommandTests
    {
        private DwapiRemoteContext _context;
        private ILoadPatientVisitExtractCommand _extractCommand;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext();
          
            _extractCommand = new LoadPatientVisitExtractCommand(new EMRRepository(_context));

            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientVisitExtract");
            

        }

        [Test]
        public void should_Execute_For_MSSQL()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            _extractCommand.Execute();
            watch.Stop();
            var records = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM TempPatientVisitExtract")
                .Single();

            Assert.IsTrue(records >0);

            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Loaded {records} records! in {elapsedMs}ms ({elapsedMs/1000}s)");

        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientVisitExtract");

        }
    }
}
