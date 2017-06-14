using System;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Command;
using PalladiumDwh.ClientReader.Infrastructure.Data.Repository;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data.Command
{
    public class ClearExtractsCommandTests
    {
        private DwapiRemoteContext _context;
        private IClearExtractsCommand _extractCommand;
        private Progress<DProgress> _dprogress;


        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext();
            _extractCommand = new ClearExtractsCommand(new EMRRepository(_context));
            _dprogress = new Progress<DProgress>(ReportDProgress);
        }

       [Test]
        public void should_Execute_ClearExtracts_Command_Async()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var deletedrecords=_extractCommand.ExecuteAsync(_dprogress).Result;
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            var records = _context.Database
                .SqlQuery<int>("SELECT (SELECT COUNT(Id) as NumOfTempRecords FROM TempPatientArtExtract)+(SELECT COUNT(Id) as NumOfRecords FROM PatientArtExtract)")
                .Single();

            Assert.IsTrue(records == 0);
            Assert.IsTrue(deletedrecords > -1);
            Console.WriteLine($"Deleted {deletedrecords} records ASYNC in  {elapsedMs * 0.001} s!");
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientArtExtract");
            _context.SaveChanges();
        }

        private void ReportDProgress(DProgress value)
        {
            Console.WriteLine(value);
        }
    }
}
