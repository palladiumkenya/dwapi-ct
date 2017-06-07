using System;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Command;
using PalladiumDwh.ClientReader.Infrastructure.Data.Repository;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data.Command
{
    public class LoadPatientPharmacyExtractDbCommandTests
    {
        private DwapiRemoteContext _context;
        private ILoadPatientPharmacyExtractCommand _extractCommand;
        

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext();
            _extractCommand = new LoadPatientPharmacyExtractCommand(new EMRRepository(_context));

            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientPharmacyExtract");
         
        }

        [Test]
        public void should_Execute_Load_PatientPharmacyExtract_DbCommand()
        {
            _extractCommand.Execute();
            var records = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM TempPatientPharmacyExtract")
                .Single();

            Assert.IsTrue(records >0);

            Console.WriteLine($"Loaded {records} records!");

        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientPharmacyExtract");
            _context.SaveChanges();
        }
    }
}
