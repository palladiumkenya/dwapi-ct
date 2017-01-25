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
    public class ReadPatientVisitExtractDbCommandTests
    {
        private IDbConnection _connection;
        private string _commandText;
        private IReadPatientVisitExtractCommand _extractCommand;

        [SetUp]
        public void SetUp()
        {
            _commandText = @"

SELECT 
  REPLACE(tmp_PatientMaster.PatientID, ' ', '') AS PatientID ,
  tmp_PatientMaster.FacilityName,
  tmp_PatientMaster.[SiteCode],
  tmp_ClinicalEncounters.PatientPK,
  tmp_ClinicalEncounters.VisitID,
  tmp_ClinicalEncounters.VisitDate,
  tmp_ClinicalEncounters.Service,
  tmp_ClinicalEncounters.VisitType,
  tmp_ClinicalEncounters.WHOStage,
  tmp_ClinicalEncounters.WABStage,
  tmp_ClinicalEncounters.Pregnant,
  tmp_ClinicalEncounters.LMP,
  tmp_ClinicalEncounters.EDD,
  tmp_ClinicalEncounters.Height,
  tmp_ClinicalEncounters.Weight,
  tmp_ClinicalEncounters.BP,
  tmp_ClinicalEncounters.OI,
  tmp_ClinicalEncounters.OIDate,
  tmp_ClinicalEncounters.Adherence,
  tmp_ClinicalEncounters.AdherenceCategory,
  NULL as SubstitutionFirstlineRegimenDate,
  NULL as SubstitutionFirstlineRegimenReason,
  NULL as SubstitutionSecondlineRegimenDate,
  NULL as SubstitutionSecondlineRegimenReason,
  NULL as SecondlineRegimenChangeDate,
  NULL as SecondlineRegimenChangeReason,
  tmp_ClinicalEncounters.FamilyPlanningMethod,
  tmp_ClinicalEncounters.PwP,
  tmp_ClinicalEncounters.GestationAge,
  tmp_ClinicalEncounters.NextAppointmentDate,
  CAST(getdate() AS DATE) AS DateExtracted
From tmp_ClinicalEncounters
  INNER JOIN tmp_PatientMaster On tmp_PatientMaster.PatientPK = tmp_ClinicalEncounters.PatientPK
 ";
        }

        [Test]
        public void should_Execute_For_MSSQL()
        {
            var connection = ConfigurationManager.ConnectionStrings["EMRDatabase"].ConnectionString;
            _connection = new SqlConnection(connection);
            _extractCommand = new ReadPatientVisitExtractDbCommand(_connection, $"{_commandText}");

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
