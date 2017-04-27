﻿using System;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Command;
using PalladiumDwh.ClientReader.Infrastructure.Data.Repository;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data.Command
{
    public class LoadPatientLaboratoryExtractDbCommandTests
    {
        private DwapiRemoteContext _context;
        
        private ILoadPatientLaboratoryExtractCommand _extractCommand;
        

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext();
          
            _extractCommand = new LoadPatientLaboratoryExtractCommand(new EMRRepository(_context));

            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientLaboratoryExtract");
            
        }

        [Test]
        public void should_Execute_Load_PatientLaboratoryExtract_DbCommand()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            _extractCommand.Execute();
            watch.Stop();
            var records = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM TempPatientLaboratoryExtract")
                .Single();

            Assert.IsTrue(records >0);

            
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Loaded {records} records! in {elapsedMs}ms ({elapsedMs / 1000}s)");

        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientLaboratoryExtract");
            _context.SaveChanges();
        }
    }
}
