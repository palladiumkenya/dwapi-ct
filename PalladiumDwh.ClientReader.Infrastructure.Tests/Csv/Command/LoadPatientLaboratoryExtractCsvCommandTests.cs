using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Infrastructure.Csv.Command;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Csv.Command
{
    public class LoadPatientLaboratoryExtractCsvCommandTests
    {
        private DwapiRemoteContext _context;
        private IDbConnection _sourceConnection, _clientConnection;
        private string _commandText;
        private ILoadPatientLaboratoryExtractCsvCommand _extractCommand;
        private int top = 5;
        private Progress<DProgress> _dprogress;


        [SetUp]
        public void SetUp()
        {
            _context=new DwapiRemoteContext();
            _dprogress = new Progress<DProgress>(ReportDProgress);
            _sourceConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EMRDatabase"].ConnectionString);
            _clientConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DWAPIRemote"].ConnectionString);

            _commandText = TestHelpers.GetCsv("PatientLaboratoryExtract");
            _extractCommand = new LoadPatientLaboratoryExtractCsvCommand(_clientConnection, $"{_commandText}");

            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientLaboratoryExtract");          
        }

        [Test]
        public void should_Execute_From_Csv()
        {
            Assert.That(_commandText, Does.Exist);
            
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var summary = _extractCommand.ExecuteLoadAsync(_dprogress).Result;
            watch.Stop();
            var records = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM TempPatientLaboratoryExtract")
                .Single();
            Assert.IsTrue(records>0);
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Loaded {records} records! in {elapsedMs}ms ({elapsedMs/1000}s)");
            Console.WriteLine(_commandText);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientLaboratoryExtract");
            _context.SaveChanges();

        }
        private void ReportDProgress(DProgress value)
        {
            Console.WriteLine(value);
        }
    }
}
