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
    public class LoadPatientExtractDbCommandTests
    {
        private DwapiRemoteContext _context;
        private IDbConnection _sourceConnection, _clientConnection;
        private string _commandText;
        private ILoadPatientExtractCommand _extractCommand;


        [SetUp]
        public void SetUp()
        {
            _context=new DwapiRemoteContext();
            _commandText = @"
            SELECT  
	            PatientID, PatientPK, FacilityID, SiteCode, FacilityName, SatelliteName, Gender, DOB, RegistrationDate, RegistrationAtCCC, RegistrationAtPMTCT, RegistrationAtTBClinic, PatientSource, Region, District, Village, 
	            ContactRelation, LastVisit, MaritalStatus, EducationLevel, DateConfirmedHIVPositive, PreviousARTExposure, PreviousARTStartDate, StatusAtCCC, StatusAtPMTCT, StatusAtTBClinic, 'IQCare' AS EMR, 
	            'Kenya HMIS II' AS Project, CAST(GETDATE() AS DATE) AS DateExtracted,newid() as ID
            FROM            
	            tmp_PatientMaster AS a
            ";
            _sourceConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EMRDatabase"].ConnectionString);
            _clientConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DWAPIRemote"].ConnectionString);

            _extractCommand = new LoadPatientExtractDbCommand(_sourceConnection,_clientConnection, $"{_commandText}");

            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientExtract");
            _context.SaveChanges();
        }

        [Test]
        public void should_Execute_For_MSSQL()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            _extractCommand.Execute();
            watch.Stop();
            var records = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM TempPatientExtract")
                .Single();
            
            Assert.IsTrue(records>0);

            
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Loaded {records} records! in {elapsedMs}ms ({elapsedMs/1000}s)");

        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientExtract");
            _context.SaveChanges();
        }
    }
}
