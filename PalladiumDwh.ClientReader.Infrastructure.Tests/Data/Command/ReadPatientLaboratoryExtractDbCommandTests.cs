using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Command;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data.Command
{
    public class ReadPatientLaboratoryExtractDbCommandTests
    {
        private DwapiRemoteContext _context;
        private IDbConnection _sourceConnection, _clientConnection;
        private string _commandText;
        private ILoadPatientLaboratoryExtractCommand _extractCommand;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext();
            _commandText = @"
                SELECT        
	                tmp_PatientMaster.PatientID, tmp_Labs.PatientPK, tmp_PatientMaster.FacilityID, tmp_PatientMaster.SiteCode, tmp_PatientMaster.FacilityName, tmp_PatientMaster.SatelliteName, tmp_Labs.VisitID, 
	                tmp_Labs.OrderedbyDate, tmp_Labs.ReportedbyDate, tmp_Labs.TestName, tmp_Labs.EnrollmentTest, tmp_Labs.TestResult, CAST(GETDATE() AS DATE) AS DateExtracted
                FROM           
	                tmp_Labs INNER JOIN
	                tmp_PatientMaster ON tmp_Labs.PatientPK = tmp_PatientMaster.PatientPK
            ";
            _sourceConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EMRDatabase"].ConnectionString);
            _clientConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DWAPIRemote"].ConnectionString);

            _extractCommand = new LoadPatientLaboratoryExtractDbCommand(_sourceConnection, _clientConnection, $"{_commandText}", 10246);

            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientLaboratoryExtract");
            _context.SaveChanges();
        }

        [Test]
        public void should_Execute_For_MSSQ0L()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            _extractCommand.Execute();
            watch.Stop();
            var records = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM TempPatientLaboratoryExtract")
                .Single();

            Assert.IsTrue(records > 0);

            
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
