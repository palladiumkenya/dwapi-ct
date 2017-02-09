using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.ClientReader.Core.Services;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Command;
using PalladiumDwh.ClientReader.Infrastructure.Data.Repository;


namespace PalladiumDwh.ClientReader.Core.Tests.Services
{
    [TestFixture]
    public class SyncServiceTests
    {
        private readonly string _cn = ConfigurationManager.ConnectionStrings["DWAPIRemote"].ConnectionString;
        private readonly string _srcCn = ConfigurationManager.ConnectionStrings["EMRDatabase"].ConnectionString;

        private ISyncService _syncService;

        private DwapiRemoteContext _context;

        private IClearExtractsCommand _clearExtractsCommand;
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
        private Interfaces.Commands.ISyncPatientLaboratoryExtractCommand _syncPatientLaboratoryExtractCommand;
        private ISyncPatientPharmacyExtractCommand _syncPatientPharmacyExtractCommand;
        private ISyncPatientVisitExtractCommand _syncPatientVisitExtractCommand;
        private ISyncPatientStatusExtractCommand _syncPatientStatusExtractCommand;
        private int top = 10;
        private int topExtracts = -1;

        [SetUp]
        public void should_SetUp()
        {
            _context = new DwapiRemoteContext();
            _clearExtractsCommand=new ClearExtractsCommand(new EMRRepository(_context));
            _loadPatientExtractCommand = new LoadPatientExtractCommand(new EMRRepository(_context));
            _loadPatientArtExtractCommand = new LoadPatientArtExtractCommand(new EMRRepository(_context));
            _loadPatientBaselinesExtractCommand = new LoadPatientBaselinesExtractCommand(new EMRRepository(_context));
            _loadPatientLaboratoryExtractCommand = new LoadPatientLaboratoryExtractCommand(new EMRRepository(_context));
            _loadPatientPharmacyExtractCommand = new LoadPatientPharmacyExtractCommand(new EMRRepository(_context));
            _loadPatientVisitExtractCommand = new LoadPatientVisitExtractCommand(new EMRRepository(_context));
            _loadPatientStatusExtractCommand = new LoadPatientStatusExtractCommand(new EMRRepository(_context));

            _syncPatientExtractCommand = new SyncPatientExtractCommand(new EMRRepository(_context));
            _syncPatientArtExtractCommand = new SyncPatientArtExtractCommand(new EMRRepository(_context));
            _syncPatientBaselinesExtractCommand = new SyncPatientBaselinesExtractCommand(new EMRRepository(_context));
            _syncPatientLaboratoryExtractCommand = new SyncPatientLaboratoryExtractCommand(new EMRRepository(_context));
            _syncPatientPharmacyExtractCommand = new SyncPatientPharmacyExtractCommand(new EMRRepository(_context));
            _syncPatientVisitExtractCommand = new SyncPatientVisitExtractCommand(new EMRRepository(_context));
            _syncPatientStatusExtractCommand = new SyncPatientStatusExtractCommand(new EMRRepository(_context));

            _syncService = new SyncService(
                _clearExtractsCommand,
                _loadPatientExtractCommand, _loadPatientArtExtractCommand, _loadPatientBaselinesExtractCommand,
                _loadPatientLaboratoryExtractCommand, _loadPatientPharmacyExtractCommand,
                _loadPatientStatusExtractCommand, _loadPatientVisitExtractCommand,
                _syncPatientExtractCommand, _syncPatientArtExtractCommand, _syncPatientBaselinesExtractCommand,
                _syncPatientLaboratoryExtractCommand, _syncPatientPharmacyExtractCommand,
                _syncPatientVisitExtractCommand, _syncPatientStatusExtractCommand);


            

            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientExtract;DELETE FROM  PatientExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientArtExtract;DELETE FROM  PatientArtExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientBaselinesExtract;DELETE FROM  PatientBaselinesExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientLaboratoryExtract;DELETE FROM  PatientLaboratoryExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientPharmacyExtract;DELETE FROM  PatientPharmacyExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientVisitExtract;DELETE FROM  PatientVisitExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientStatusExtract;DELETE FROM  PatientStatusExtract");

            

        }

        
        [TestCase(nameof(TempPatientArtExtract))]
        [TestCase(nameof(TempPatientBaselinesExtract))]
        [TestCase(nameof(TempPatientStatusExtract))]
        [TestCase(nameof(TempPatientVisitExtract))]
        [TestCase(nameof(TempPatientPharmacyExtract))]
        [TestCase(nameof(TempPatientLaboratoryExtract))]

        public void should_Sync(string extract)
        {
            _syncService.Initialize();
            _syncService.Sync(nameof(TempPatientExtract));

            var summary=_syncService.Sync(extract);
            Assert.IsTrue(summary.LoadSummary.Total > 0);
            Assert.IsTrue(summary.LoadSummary.Loaded> 0);
            Assert.IsTrue(summary.SyncSummary.Total > 0);
            Console.WriteLine($"{extract}: {summary}");
            Assert.IsTrue(_context.ClientPatientExtracts.ToList().Count > 0);
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