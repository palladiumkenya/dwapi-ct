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
    public class ReadPatientArtExtractDbCommandTests
    {
        private DwapiRemoteContext _context;
        private IDbConnection _sourceConnection, _clientConnection;
        private string _commandText;
        private ILoadPatientArtExtractCommand _extractCommand;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext();
            _commandText = @"

                SELECT TOP 5       
	                a.PatientPK, a.PatientID, c.FacilityID, c.SiteCode, a.FacilityName, a.DOB, a.AgeEnrollment, a.AgeARTStart, a.AgeLastVisit, a.RegistrationDate, a.PatientSource, a.Gender, a.StartARTDate, a.PreviousARTStartDate, 
	                a.PreviousARTRegimen, a.StartARTAtThisFacility, a.StartRegimen, a.StartRegimenLine, a.LastARTDate, a.LastRegimen, a.LastRegimenLine, a.Duration, a.ExpectedReturn, a.Provider, a.LastVisit, a.ExitReason, 
	                a.ExitDate, CAST(GETDATE() AS DATE) AS DateExtracted
                FROM            
	                tmp_ARTPatients AS a INNER JOIN
	                tmp_PatientMaster AS c ON a.PatientPK = c.PatientPK

            ";

            _sourceConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EMRDatabase"].ConnectionString);
            _clientConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DWAPIRemote"].ConnectionString);

            _extractCommand = new LoadPatientArtExtractDbCommand(_sourceConnection, _clientConnection, $"{_commandText}");

            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientArtExtract");
            _context.SaveChanges();
        }

      
        [Test]
        public void should_Execute_For_MSSQL()
        {
            _extractCommand.Execute();
            var records = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM TempPatientArtExtract")
                .Single();

            Assert.IsTrue(records == 5);

            Console.WriteLine($"Loaded {records} records!");

        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientArtExtract");
            _context.SaveChanges();
        }
    }
}
