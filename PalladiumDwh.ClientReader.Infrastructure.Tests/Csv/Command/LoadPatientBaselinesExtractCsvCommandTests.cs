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
    public class LoadPatientBaselinesExtractCsvCommandTests
    {
        private DwapiRemoteContext _context;
        private IDbConnection _clientConnection;
        private string _commandText;
        private ILoadPatientBaselinesExtractCommand _extractCommand;
        private int top = 5;

        

        [SetUp]
        public void SetUp()
        {
            _context=new DwapiRemoteContext();
            
            new SqlConnection(ConfigurationManager.ConnectionStrings["EMRDatabase"].ConnectionString);
            _clientConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DWAPIRemote"].ConnectionString);

            _commandText = TestHelpers.GetCsv("PatientWABWHOCD4Extract");

            _extractCommand = new LoadPatientBaselinesExtractCsvCommand(_clientConnection, $"{_commandText}");

            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientBaselinesExtract");          
        }

        [Test]
        public void should_Execute_From_Csv()
        {
            Assert.That(_commandText, Does.Exist);
            
            var watch = System.Diagnostics.Stopwatch.StartNew();
            _extractCommand.Execute();
            watch.Stop();
            var records = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM TempPatientBaselinesExtract")
                .Single();
            Assert.IsTrue(records>0);
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Loaded {records} records! in {elapsedMs}ms ({elapsedMs/1000}s)");
            Console.WriteLine(_commandText);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientBaselinesExtract");
            _context.SaveChanges();
        }
    }
}
