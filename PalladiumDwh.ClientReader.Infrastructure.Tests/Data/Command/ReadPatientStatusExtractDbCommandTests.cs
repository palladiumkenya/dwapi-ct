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
    public class ReadPatientStatusExtractDbCommandTests
    {
        private DwapiRemoteContext _context;
        private IDbConnection _sourceConnection, _clientConnection;
        private string _commandText;
        private ILoadPatientStatusExtractCommand _extractCommand;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext();
            _commandText = @"
                SELECT     TOP 5   
	                tmp_PatientMaster.PatientID, tmp_LastStatus.PatientPK, tmp_PatientMaster.SiteCode, tmp_PatientMaster.FacilityName, tmp_LastStatus.ExitDescription, tmp_LastStatus.ExitDate, tmp_LastStatus.ExitReason, 
	                CAST(GETDATE() AS DATE) AS DateExtracted
                FROM            
	                tmp_LastStatus INNER JOIN
	                tmp_PatientMaster ON tmp_LastStatus.PatientPK = tmp_PatientMaster.PatientPK ";

            _sourceConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EMRDatabase"].ConnectionString);
            _clientConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DWAPIRemote"].ConnectionString);

            _extractCommand = new LoadPatientStatusExtractDbCommand(_sourceConnection, _clientConnection, $"{_commandText}");

            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientStatusExtract");
            _context.SaveChanges();
        }

        [Test]
        public void should_Execute_For_MSSQL()
        {
            _extractCommand.Execute();
            var records = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM TempPatientStatusExtract")
                .Single();

            Assert.IsTrue(records == 5);

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
