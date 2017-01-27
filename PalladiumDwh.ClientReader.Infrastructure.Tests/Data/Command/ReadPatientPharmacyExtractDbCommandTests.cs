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
    public class ReadPatientPharmacyExtractDbCommandTests
    {
        private DwapiRemoteContext _context;
        private IDbConnection _sourceConnection, _clientConnection;
        private string _commandText;
        private ILoadPatientPharmacyExtractCommand _extractCommand;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext();
            _commandText = @"
                SELECT TOP 5
	                tmp_PatientMaster.PatientID, tmp_PatientMaster.FacilityID, tmp_PatientMaster.SiteCode, tmp_Pharmacy.PatientPK, tmp_Pharmacy.VisitID, tmp_Pharmacy.Drug, tmp_Pharmacy.Provider, 
	                tmp_Pharmacy.DispenseDate, tmp_Pharmacy.Duration, tmp_Pharmacy.ExpectedReturn, tmp_Pharmacy.TreatmentType, tmp_Pharmacy.RegimenLine, tmp_Pharmacy.PeriodTaken, 
	                tmp_Pharmacy.ProphylaxisType, CAST(GETDATE() AS DATE) AS DateExtracted
                FROM            
	                tmp_Pharmacy INNER JOIN
                    tmp_PatientMaster ON tmp_Pharmacy.PatientPK = tmp_PatientMaster.PatientPK  ";
            _sourceConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EMRDatabase"].ConnectionString);
            _clientConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DWAPIRemote"].ConnectionString);

            _extractCommand = new LoadPatientPharmacyExtractDbCommand(_sourceConnection, _clientConnection, $"{_commandText}");

            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientPharmacyExtract");
            _context.SaveChanges();
        }

        [Test]
        public void should_Execute_For_MSSQL()
        {
            _extractCommand.Execute();
            var records = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM TempPatientPharmacyExtract")
                .Single();

            Assert.IsTrue(records == 5);

            Console.WriteLine($"Loaded {records} records!");

        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientPharmacyExtract");
            _context.SaveChanges();
        }
    }
}
