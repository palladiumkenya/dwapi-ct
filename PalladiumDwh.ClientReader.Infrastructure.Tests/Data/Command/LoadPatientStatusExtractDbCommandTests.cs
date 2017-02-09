﻿using System;
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
    public class LoadPatientStatusExtractDbCommandTests
    {
        private DwapiRemoteContext _context;
        private ILoadPatientStatusExtractCommand _extractCommand;


        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext();
          
            _extractCommand = new LoadPatientStatusExtractCommand(new EMRRepository(_context)); ;

            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientStatusExtract");
            
        }

        [Test]
        public void should_Execute_For_MSSQL()
        {
            _extractCommand.Execute();
            var records = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM TempPatientStatusExtract")
                .Single();

            Assert.IsTrue(records >0);

            Console.WriteLine($"Loaded {records} records!");

        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientStatusExtract");
            _context.SaveChanges();
        }
    }
}
