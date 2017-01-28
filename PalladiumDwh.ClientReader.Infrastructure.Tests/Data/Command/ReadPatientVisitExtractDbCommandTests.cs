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
    public class ReadPatientVisitExtractDbCommandTests
    {
        private DwapiRemoteContext _context;
        private IDbConnection _sourceConnection, _clientConnection;
        private string _commandText;
        private ILoadPatientVisitExtractCommand _extractCommand;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext();
            //_context.Database.ExecuteSqlCommand("DELETE FROM TempPatientVisitExtract");
            _context.Configuration.AutoDetectChangesEnabled = false;

            _commandText = @"

                SELECT       
	                REPLACE(tmp_PatientMaster.PatientID, ' ', '') AS PatientID, tmp_PatientMaster.FacilityName, tmp_PatientMaster.SiteCode, tmp_ClinicalEncounters.PatientPK, tmp_ClinicalEncounters.VisitID, 
	                tmp_ClinicalEncounters.VisitDate, tmp_ClinicalEncounters.Service, tmp_ClinicalEncounters.VisitType, tmp_ClinicalEncounters.WHOStage, tmp_ClinicalEncounters.WABStage, tmp_ClinicalEncounters.Pregnant, 
	                tmp_ClinicalEncounters.LMP, tmp_ClinicalEncounters.EDD, tmp_ClinicalEncounters.Height, tmp_ClinicalEncounters.Weight, tmp_ClinicalEncounters.BP, tmp_ClinicalEncounters.OI, 
	                tmp_ClinicalEncounters.OIDate, tmp_ClinicalEncounters.Adherence, tmp_ClinicalEncounters.AdherenceCategory, NULL AS SubstitutionFirstlineRegimenDate, NULL AS SubstitutionFirstlineRegimenReason, NULL 
	                AS SubstitutionSecondlineRegimenDate, NULL AS SubstitutionSecondlineRegimenReason, NULL AS SecondlineRegimenChangeDate, NULL AS SecondlineRegimenChangeReason, 
	                tmp_ClinicalEncounters.FamilyPlanningMethod, tmp_ClinicalEncounters.PwP, tmp_ClinicalEncounters.GestationAge, tmp_ClinicalEncounters.NextAppointmentDate, CAST(GETDATE() AS DATE) 
	                AS DateExtracted
                FROM            
	                tmp_ClinicalEncounters INNER JOIN
	                tmp_PatientMaster ON tmp_PatientMaster.PatientPK = tmp_ClinicalEncounters.PatientPK
                ";
            _sourceConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EMRDatabase"].ConnectionString);
            _clientConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DWAPIRemote"].ConnectionString);

            //_extractCommand = new LoadPatientVisitExtractDbCommand(_sourceConnection, _clientConnection, $"{_commandText}", 500);
            _extractCommand = new EFLoadPatientVisitExtractDbCommand(_sourceConnection, _context, $"{_commandText}");
            
        }

        [Test]
        public void should_Execute_For_MSSQL()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            _extractCommand.Execute();
            watch.Stop();
            var records = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM TempPatientVisitExtract")
                .Single();

            Assert.IsTrue(records > 0);

            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Loaded {records} records! in {elapsedMs}ms ({elapsedMs / 1000}s)");

        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientVisitExtract");
            
        }
    }
}
