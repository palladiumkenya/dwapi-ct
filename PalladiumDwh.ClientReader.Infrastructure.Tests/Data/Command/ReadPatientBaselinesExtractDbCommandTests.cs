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
    public class ReadPatientBaselinesExtractDbCommandTests
    {
        private DwapiRemoteContext _context;
        private IDbConnection _sourceConnection, _clientConnection;
        private string _commandText;
        private ILoadPatientBaselinesExtractCommand _extractCommand;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext();
            _commandText = @"
                SELECT      TOP 5
	                tmp_PatientMaster.PatientPK, tmp_PatientMaster.PatientID, tmp_PatientMaster.FacilityID, tmp_PatientMaster.SiteCode, IQC_bCD4.bCD4, IQC_bCD4.bCD4Date, IQC_bWAB.bWAB, IQC_bWAB.bWABDate, 
	                IQC_bWHO.bWHO, IQC_bWHO.bWHODate, IQC_eWAB.eWAB, IQC_eWAB.eWABDate, IQC_eCD4.eCD4, IQC_eCD4.eCD4Date, IQC_eWHO.eWHO, IQC_eWHO.eWHODate, IQC_lastWHO.lastWHO, 
	                IQC_lastWHO.lastWHODate, IQC_lastWAB.lastWAB, IQC_lastWAB.lastWABDate, IQC_lastCD4.lastCD4, IQC_lastCD4.lastCD4Date, IQC_m12CD4.m12CD4, IQC_m12CD4.m12CD4Date, IQC_m6CD4.m6CD4, 
	                IQC_m6CD4.m6CD4Date, CAST(GETDATE() AS DATE) AS DateExtracted
                FROM            
	                tmp_PatientMaster LEFT OUTER JOIN
                    IQC_bCD4 ON tmp_PatientMaster.PatientPK = IQC_bCD4.PatientPK LEFT OUTER JOIN
                    IQC_bWAB ON tmp_PatientMaster.PatientPK = IQC_bWAB.PatientPK LEFT OUTER JOIN
                    IQC_bWHO ON tmp_PatientMaster.PatientPK = IQC_bWHO.PatientPK LEFT OUTER JOIN
                    IQC_lastCD4 ON tmp_PatientMaster.PatientPK = IQC_lastCD4.PatientPK LEFT OUTER JOIN
                    IQC_eWAB ON tmp_PatientMaster.PatientPK = IQC_eWAB.PatientPK LEFT OUTER JOIN
                    IQC_eWHO ON tmp_PatientMaster.PatientPK = IQC_eWHO.PatientPK LEFT OUTER JOIN
                    IQC_lastWAB ON tmp_PatientMaster.PatientPK = IQC_lastWAB.PatientPK LEFT OUTER JOIN
                    IQC_eCD4 ON tmp_PatientMaster.PatientPK = IQC_eCD4.PatientPK LEFT OUTER JOIN
                    IQC_lastWHO ON tmp_PatientMaster.PatientPK = IQC_lastWHO.PatientPK LEFT OUTER JOIN
                    IQC_m12CD4 ON tmp_PatientMaster.PatientPK = IQC_m12CD4.PatientPK LEFT OUTER JOIN
                    IQC_m6CD4 ON tmp_PatientMaster.PatientPK = IQC_m6CD4.PatientPK";

            _sourceConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["EMRDatabase"].ConnectionString);
            _clientConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DWAPIRemote"].ConnectionString);

            _extractCommand = new LoadPatientBaselinesExtractDbCommand(_sourceConnection, _clientConnection, $"{_commandText}");

            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientBaselinesExtract");
            _context.SaveChanges();
        }

        [Test]
        public void should_Execute_For_MSSQL()
        {
            _extractCommand.Execute();
            var records = _context.Database
                .SqlQuery<int>("SELECT COUNT(*) as NumOfRecords FROM TempPatientBaselinesExtract")
                .Single();

            Assert.IsTrue(records == 5);

            Console.WriteLine($"Loaded {records} records!");

        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientBaselinesExtract");
            _context.SaveChanges();
        }
    }
}
