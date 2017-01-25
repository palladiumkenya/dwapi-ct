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
    public class ReadPatientBaselinesExtractDbCommandTests
    {
        private IDbConnection _connection;
        private string _commandText;
        private IReadPatientBaselinesExtractCommand _extractCommand;

        [SetUp]
        public void SetUp()
        {
            _commandText = @"
Select 
  tmp_PatientMaster.PatientPK,
  tmp_PatientMaster.PatientID,
  tmp_PatientMaster.[FacilityID],
  tmp_PatientMaster.[SiteCode], 
  IQC_bCD4.bCD4,
  IQC_bCD4.bCD4Date,
  IQC_bWAB.bWAB,
  IQC_bWAB.bWABDate,
  IQC_bWHO.bWHO,
  IQC_bWHO.bWHODate,
  IQC_eWAB.eWAB,
  IQC_eWAB.eWABDate,
  IQC_eCD4.eCD4,
  IQC_eCD4.eCD4Date,
  IQC_eWHO.eWHO,
  IQC_eWHO.eWHODate,
  IQC_lastWHO.lastWHO,
  IQC_lastWHO.lastWHODate,
  IQC_lastWAB.lastWAB,
  IQC_lastWAB.lastWABDate,
  IQC_lastCD4.lastCD4,
  IQC_lastCD4.lastCD4Date,
  IQC_m12CD4.m12CD4,
  IQC_m12CD4.m12CD4Date,
  IQC_m6CD4.m6CD4,
  IQC_m6CD4.m6CD4Date,
  CAST(getdate() AS DATE) AS DateExtracted
From tmp_PatientMaster 
  Left Join IQC_bCD4 On tmp_PatientMaster.PatientPK = IQC_bCD4.PatientPK
  Left Join IQC_bWAB On tmp_PatientMaster.PatientPK = IQC_bWAB.PatientPK
  Left Join IQC_bWHO On tmp_PatientMaster.PatientPK = IQC_bWHO.PatientPK
  Left Join IQC_lastCD4 On tmp_PatientMaster.PatientPK = IQC_lastCD4.PatientPK
  Left Join IQC_eWAB On tmp_PatientMaster.PatientPK = IQC_eWAB.PatientPK
  Left Join IQC_eWHO On tmp_PatientMaster.PatientPK = IQC_eWHO.PatientPK
  Left Join IQC_lastWAB On tmp_PatientMaster.PatientPK = IQC_lastWAB.PatientPK
  Left Join IQC_eCD4 On tmp_PatientMaster.PatientPK = IQC_eCD4.PatientPK
  Left Join IQC_lastWHO On tmp_PatientMaster.PatientPK = IQC_lastWHO.PatientPK
  Left Join IQC_m12CD4 On tmp_PatientMaster.PatientPK = IQC_m12CD4.PatientPK
  Left Join IQC_m6CD4 On tmp_PatientMaster.PatientPK = IQC_m6CD4.PatientPK
";
        }

        [Test]
        public void should_Execute_For_MSSQL()
        {
            var connection = ConfigurationManager.ConnectionStrings["EMRDatabase"].ConnectionString;
            _connection = new SqlConnection(connection);
            _extractCommand = new ReadPatientBaselinesExtractDbCommand(_connection, $"{_commandText}");

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
