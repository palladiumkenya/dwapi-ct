using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using MySql.Data.MySqlClient;
using Npgsql;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Infrastructure.Data;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data
{
    public class ReadPatientArtExtractDbCommandTests
    {
        private IDbConnection _connection;
        private string _commandText;
        private IReadPatientArtExtractCommand _extractCommand;

        [SetUp]
        public void SetUp()
        {
            _commandText = @"
SELECT a.[PatientPK]
      ,a.[PatientID]
       ,c.[FacilityID]
      ,c.[SiteCode]
      ,a.[FacilityName]
      ,a.[DOB]
      ,a.[AgeEnrollment]
      ,a.[AgeARTStart]
      ,a.[AgeLastVisit]
      ,a.[RegistrationDate]
      ,a.[PatientSource]
      ,a.[Gender]
      ,[StartARTDate]
      ,a.[PreviousARTStartDate]
      ,[PreviousARTRegimen]
      ,[StartARTAtThisFacility]
      ,[StartRegimen]
      ,[StartRegimenLine]
      ,[LastARTDate]
      ,[LastRegimen]
      ,[LastRegimenLine]
      ,[Duration]
      ,[ExpectedReturn]
      ,[Provider]
      ,a.[LastVisit]
      ,[ExitReason]
      ,[ExitDate]
	  ,CAST(getdate() AS DATE) AS DateExtracted
  FROM dbo.[tmp_ARTPatients] a
  INNER JOIN dbo.[tmp_PatientMaster] c ON a.PatientPK=c.PatientPK
";
        }

        [Test]
        public void should_Execute_For_MSSQL()
        {
            var connection = ConfigurationManager.ConnectionStrings["EMRDatabase"].ConnectionString;
            _connection = new SqlConnection(connection);
            _extractCommand = new ReadPatientArtExtractDbCommand(_connection, $"{_commandText}");

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
