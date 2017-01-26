using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Infrastructure.Data.Command;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data.Command
{
    public class ReadPatientExtractDbCommandTests
    {
        private IDbConnection _connection;
        private string _commandText;
        private IReadPatientExtractCommand _extractCommand;

        [SetUp]
        public void SetUp()
        {
         
            _commandText = @"
SELECT
	 [PatientID]
      ,[PatientPK]
      ,a.[FacilityID]
      ,a.[SiteCode]
      ,a.[FacilityName]
      ,[SatelliteName]
      ,[Gender]
      ,[DOB]
      ,[RegistrationDate]
      ,[RegistrationAtCCC]
      ,[RegistrationAtPMTCT]
      ,[RegistrationAtTBClinic]
      ,[PatientSource]
      ,[Region]
      ,[District]
      ,[Village]
      ,[ContactRelation]
      ,[LastVisit]
      ,[MaritalStatus]
      ,[EducationLevel]
      ,[DateConfirmedHIVPositive]
      ,[PreviousARTExposure]
      ,[PreviousARTStartDate]
      ,[StatusAtCCC]
      ,[StatusAtPMTCT]
      ,[StatusAtTBClinic]
	  ,'IQCare' AS EMR
	  ,'Kenya HMIS II' AS Project
	  ,CAST(getdate() AS DATE) AS DateExtracted
  FROM  
	dbo.tmp_PatientMaster a
  
";
            
        }

        [Test]
        public void should_Execute_For_MSSQL()
        {
            var connection = ConfigurationManager.ConnectionStrings["EMRDatabase"].ConnectionString;
            _connection = new SqlConnection(connection);
            _extractCommand = new ReadPatientExtractDbCommand(_connection, $"{_commandText}");

            var list = _extractCommand.Execute().ToList();
            Assert.IsTrue(list.Count > 0);

            Console.WriteLine($"Loaded {list.Count} records!");

        }
        /*
        [Test]
        public void should_Execute_For_MySQL()
        {
            var connection = ConfigurationManager.ConnectionStrings["MySQLEMRDatabase"].ConnectionString;
            _connection = new MySqlConnection(connection);
            _extractCommand = new ReadPatientExtractDbCommand(_connection, $"{_commandText} tmp_PatientMaster");

            var list = _extractCommand.Execute().ToList();
            Assert.IsTrue(list.Count > 0);

            Console.WriteLine($"Loaded {list.Count} records!");
            
        }
        [Test]
        public void should_Execute_For_Postgress()
        {
            var connection = ConfigurationManager.ConnectionStrings["PostgreSQLEMRDatabase"].ConnectionString;
            _connection = new NpgsqlConnection(connection);
            _extractCommand = new ReadPatientExtractDbCommand(_connection, $"{_commandText} tmp_patientmaster".ToLower());

            var list = _extractCommand.Execute().ToList();
            Assert.IsTrue(list.Count > 0);

            Console.WriteLine($"Loaded {list.Count} records!");
        }
        */
    }
}
