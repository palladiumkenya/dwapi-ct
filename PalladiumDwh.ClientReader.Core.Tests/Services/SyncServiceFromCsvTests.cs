using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Services;
using PalladiumDwh.ClientReader.Infrastructure.Csv.Command;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Command;

namespace PalladiumDwh.ClientReader.Core.Tests.Services
{
    [TestFixture]
    public class SyncServiceFromCsvTests
    {
        private readonly string _cn = ConfigurationManager.ConnectionStrings["DWAPIRemote"].ConnectionString;
        private readonly string _srcCn = ConfigurationManager.ConnectionStrings["EMRDatabase"].ConnectionString;

        private ISyncService _syncService;

        private DwapiRemoteContext _context;

        private ILoadPatientExtractCommand _loadPatientExtractCommand;
        private ILoadPatientArtExtractCommand _loadPatientArtExtractCommand;
        private ILoadPatientBaselinesExtractCommand _loadPatientBaselinesExtractCommand;
        private ILoadPatientLaboratoryExtractCommand _loadPatientLaboratoryExtractCommand;
        private ILoadPatientPharmacyExtractCommand _loadPatientPharmacyExtractCommand;
        private ILoadPatientStatusExtractCommand _loadPatientStatusExtractCommand;
        private ILoadPatientVisitExtractCommand _loadPatientVisitExtractCommand;

        private ISyncPatientExtractCommand _syncPatientExtractCommand;
        private ISyncPatientArtExtractCommand _syncPatientArtExtractCommand;
        private ISyncPatientBaselinesExtractCommand _syncPatientBaselinesExtractCommand;
        private ISyncPatientLaboratoryExtractCommand _syncPatientLaboratoryExtractCommand;
        private ISyncPatientPharmacyExtractCommand _syncPatientPharmacyExtractCommand;
        private ISyncPatientVisitExtractCommand _syncPatientVisitExtractCommand;
        private ISyncPatientStatusExtractCommand _syncPatientStatusExtractCommand;
        private int top = 10;
        private int topExtracts = -1;

        [SetUp]
        public void should_SetUp()
        {

            _loadPatientExtractCommand = new LoadPatientExtractCsvCommand(new SqlConnection(_cn), TestHelpers.GetCsv("PatientExtract"));
            _loadPatientArtExtractCommand = new LoadPatientArtExtractCsvCommand(new SqlConnection(_cn), TestHelpers.GetCsv("ARTPatientExtract"));
            _loadPatientBaselinesExtractCommand = new LoadPatientBaselinesExtractCsvCommand(new SqlConnection(_cn), TestHelpers.GetCsv("PatientWABWHOCD4Extract"));
            _loadPatientLaboratoryExtractCommand = new LoadPatientLaboratoryExtractCsvCommand(new SqlConnection(_cn), TestHelpers.GetCsv("PatientLaboratoryExtract"));
            _loadPatientPharmacyExtractCommand = new LoadPatientPharmacyExtractCsvCommand(new SqlConnection(_cn), TestHelpers.GetCsv("PatientPharmacyExtract"));
            _loadPatientVisitExtractCommand = new LoadPatientVisitExtractCsvCommand(new SqlConnection(_cn), TestHelpers.GetCsv("PatientVisitExtract"));
            _loadPatientStatusExtractCommand = new LoadPatientStatusExtractCsvCommand(new SqlConnection(_cn), TestHelpers.GetCsv("PatientStatusExtract"));

            _syncPatientExtractCommand = new SyncPatientExtractDbCommand(_cn);
            _syncPatientArtExtractCommand = new SyncPatientArtExtractDbCommand(_cn);
            _syncPatientBaselinesExtractCommand = new SyncPatientBaselinesExtractDbCommand(_cn);
            _syncPatientLaboratoryExtractCommand = new SyncPatientLaboratoryExtractDbCommand(_cn);
            _syncPatientPharmacyExtractCommand = new SyncPatientPharmacyExtractDbCommand(_cn);
            _syncPatientVisitExtractCommand = new SyncPatientVisitExtractDbCommand(_cn);
            _syncPatientStatusExtractCommand = new SyncPatientStatusExtractDbCommand(_cn);

            _syncService = new SyncService(
                _loadPatientExtractCommand, _loadPatientArtExtractCommand, _loadPatientBaselinesExtractCommand,
                _loadPatientLaboratoryExtractCommand, _loadPatientPharmacyExtractCommand,
                _loadPatientStatusExtractCommand, _loadPatientVisitExtractCommand,
                _syncPatientExtractCommand, _syncPatientArtExtractCommand, _syncPatientBaselinesExtractCommand,
                _syncPatientLaboratoryExtractCommand, _syncPatientPharmacyExtractCommand,
                _syncPatientVisitExtractCommand, _syncPatientStatusExtractCommand);


            _context = new DwapiRemoteContext();

            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientExtract;DELETE FROM  PatientExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientArtExtract;DELETE FROM  PatientArtExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientBaselinesExtract;DELETE FROM  PatientBaselinesExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientLaboratoryExtract;DELETE FROM  PatientLaboratoryExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientPharmacyExtract;DELETE FROM  PatientPharmacyExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientVisitExtract;DELETE FROM  PatientVisitExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientStatusExtract;DELETE FROM  PatientStatusExtract");

        }

        [Test]
        public void should_SyncAll()
        {
            _syncService.SyncAll();

            
            Assert.IsTrue(_context.ClientPatientExtracts.ToList().Count > 0);
            Assert.IsTrue(_context.ClientPatientArtExtracts.ToList().Count > 0);
            Assert.IsTrue(_context.ClientPatientBaselinesExtracts.ToList().Count > 0);
            Assert.IsTrue(_context.ClientPatientLaboratoryExtracts.ToList().Count > 0);
            Assert.IsTrue(_context.ClientPatientPharmacyExtracts.ToList().Count > 0);
            Assert.IsTrue(_context.ClientPatientVisitExtracts.ToList().Count > 0);
            Assert.IsTrue(_context.ClientPatientStatusExtracts.ToList().Count > 0);
            
        }

        [Test]
        public void should_SyncPatients()
        {
            _syncService.SyncPatients();

            var extracts = _context.ClientPatientExtracts.Count();
            Assert.IsTrue(extracts > 0);
        }

        [Test]
        public void should_SynPatientsArt()
        {
            _syncService.SyncPatients();
            _syncService.SynPatientsArt();

            var extracts = _context.ClientPatientArtExtracts.Count();
            Assert.IsTrue(extracts > 0);
        }

        [Test]
        public void should_SynPatientsBaselines()
        {
            _syncService.SyncPatients();
            _syncService.SynPatientsBaselines();

            var extracts = _context.ClientPatientBaselinesExtracts.Count();
            Assert.IsTrue(extracts > 0);
        }

        [Test]
        public void should_SynPatientsLab()
        {
            _syncService.SyncPatients();
            _syncService.SynPatientsLab();

            var extracts = _context.ClientPatientLaboratoryExtracts.Count();
            Assert.IsTrue(extracts > 0);
        }

        [Test]
        public void should_SynPatientsPharmacy()
        {
            _syncService.SyncPatients();
            _syncService.SynPatientsPharmacy();

            var extracts = _context.ClientPatientPharmacyExtracts.Count();
            Assert.IsTrue(extracts > 0);
        }

        [Test]
        public void should_SynPatientsStatus()
        {
            _syncService.SyncPatients();
            _syncService.SynPatientsStatus();

            var extracts = _context.ClientPatientStatusExtracts.Count();
            Assert.IsTrue(extracts > 0);
        }


        [Test]
        public void should_SynPatientsVisits()
        {
            _syncService.SyncPatients();
            _syncService.SynPatientsVisits();

            var extracts = _context.ClientPatientVisitExtracts.Count();
            Assert.IsTrue(extracts > 0);
        }

        [TearDown]
        public void TearDown()
        {
            
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientExtract;DELETE FROM  PatientExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientArtExtract;DELETE FROM  PatientArtExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientBaselinesExtract;DELETE FROM  PatientBaselinesExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientLaboratoryExtract;DELETE FROM  PatientLaboratoryExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientPharmacyExtract;DELETE FROM  PatientPharmacyExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientStatusExtract;DELETE FROM  PatientStatusExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientVisitExtract;DELETE FROM  PatientVisitExtract");

        }
    }
}