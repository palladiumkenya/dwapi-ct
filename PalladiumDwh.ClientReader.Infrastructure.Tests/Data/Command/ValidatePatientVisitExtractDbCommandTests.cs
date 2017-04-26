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
    public class ValidatePatientVisitExtractDbCommandTests
    {
        private DwapiRemoteContext _context;
        private IValidatePatientVisitExtractCommand _extractCommand;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext();
          
            _extractCommand = new ValidatePatientVisitExtractCommand(new EMRRepository(_context), new ValidatorRepository(_context));

           _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientVisitExtract;DELETE FROM ValidationError");


        }

        [Test]
        public void should_Execute_Validate_PatientVisitExtract_DbCommand()
        {
            var result = new LoadPatientExtractCommand(new EMRRepository(_context)).ExecuteAsync().Result;
            _context.Database.ExecuteSqlCommand("UPDATE TempPatientVisitExtract SET Gender=NULL,DOB=NULL;");

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
            

            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Validated {records} records! in {elapsedMs}ms ({elapsedMs / 1000}s)");

        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientVisitExtract;DELETE FROM ValidationError");

        }
    }
}
