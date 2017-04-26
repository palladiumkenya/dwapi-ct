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
    public class ValidatePatientExtractDbCommandTests
    {
        private DwapiRemoteContext _context;
        
        private IValidatePatientExtractCommand _extractCommand;
        

        [SetUp]
        public void SetUp()
        {
            _context=new DwapiRemoteContext();
            _extractCommand = new ValidatePatientExtractCommand(new EMRRepository(_context), new ValidatorRepository(_context));
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientExtract;DELETE FROM ValidationError");
        }

        [Test]
        public void should_Execute_Validate_PatientExtract_DbCommand()
        {
            var result=new LoadPatientExtractCommand(new EMRRepository(_context)).ExecuteAsync().Result;
            _context.Database.ExecuteSqlCommand("UPDATE TempPatientExtract SET Gender=NULL,DOB=NULL;");
            
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var summary = _extractCommand.ExecuteAsync().Result;
            watch.Stop();
            var errors = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM ValidationError")
                .Single();

            var records = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM TempPatientExtract WHERE CheckError=1")
                .Single();

            Assert.IsTrue(records > 0);
            Assert.IsTrue(errors > 0);
            Assert.AreEqual(records,summary.Total);
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Validated {records} records! Found {errors} errors in {elapsedMs}ms ({elapsedMs / 1000}s)");
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientExtract;DELETE FROM ValidationError;DELETE FROM ValidationError");
            _context.SaveChanges();
        }
    }
}
