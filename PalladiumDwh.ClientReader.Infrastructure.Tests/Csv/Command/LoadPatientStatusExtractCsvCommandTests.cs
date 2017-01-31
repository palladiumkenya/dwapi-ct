using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Infrastructure.Csv.Command;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Command;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Csv.Command
{
    public class LoadPatientStatusExtractCsvCommandTests
    {
        private DwapiRemoteContext _context;
        private IDbConnection _sourceConnection, _clientConnection;
        private string _commandText;
        private ILoadPatientStatusExtractCommand _extractCommand;
        private int top = 5;

        

        [SetUp]
        public void SetUp()
        {
            _context=new DwapiRemoteContext();
            
            _sourceConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EMRDatabase"].ConnectionString);
            _clientConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DWAPIRemote"].ConnectionString);

            _commandText = TestHelpers.GetCsv("PatientStatusExtract");

            _extractCommand = new LoadPatientStatusExtractCsvCommand(_clientConnection, $"{_commandText}");

            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientStatusExtract");          
        }

        [Test]
        public void should_Execute_From_Csv()
        {
            Assert.That(_commandText, Does.Exist);
            
            var watch = System.Diagnostics.Stopwatch.StartNew();
            _extractCommand.Execute();
            watch.Stop();
            var records = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM TempPatientStatusExtract")
                .Single();
            Assert.IsTrue(records>0);
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Loaded {records} records! in {elapsedMs}ms ({elapsedMs/1000}s)");
            Console.WriteLine(_commandText);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientStatusExtract");
            _context.SaveChanges();
        }
    }
}
