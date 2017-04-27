﻿using System;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Command;
using PalladiumDwh.ClientReader.Infrastructure.Data.Repository;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data.Command
{
    public class ValidatePatientLaboratoryExtractDbCommandTests
    {
        private DwapiRemoteContext _context;
        
        private IValidatePatientLaboratoryExtractCommand _extractCommand;
        

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext();
          
            _extractCommand = new ValidatePatientLaboratoryExtractCommand(new EMRRepository(_context), new ValidatorRepository(_context));

            
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientLaboratoryExtract;DELETE FROM ValidationError");

        }

        [Test]
        public void should_Execute_Validate_PatientLaboratoryExtract_DbCommand()
        {
            var result = new LoadPatientLaboratoryExtractCommand(new EMRRepository(_context)).ExecuteAsync().Result;
            _context.Database.ExecuteSqlCommand("UPDATE TempPatientLaboratoryExtract SET SiteCode=NULL;");

            var watch = System.Diagnostics.Stopwatch.StartNew();

            var summary = _extractCommand.ExecuteAsync().Result;
            watch.Stop();
            var errorRecords = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM ValidationError")
                .Single();

            var records = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM TempPatientLaboratoryExtract WHERE CheckError=1")
                .Single();

            Assert.IsTrue(records > 0);
            Assert.IsTrue(errorRecords > 0);
            Assert.AreEqual(records, summary.Total);

            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Validated {records} records! in {elapsedMs}ms ({elapsedMs / 1000}s)");
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientLaboratoryExtract;DELETE FROM ValidationError");
            _context.SaveChanges();
        }
    }
}
