using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.ClientReader.Core.Services;
using PalladiumDwh.ClientReader.Infrastructure.Csv.Command;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Command;
using PalladiumDwh.ClientReader.Infrastructure.Data.Repository;


namespace PalladiumDwh.ClientReader.Core.Tests.Services
{
    [TestFixture]
    public class SyncCsvServiceTests
    {
        private readonly string _cn = ConfigurationManager.ConnectionStrings["DWAPIRemote"].ConnectionString;
        private readonly string _srcCn = ConfigurationManager.ConnectionStrings["EMRDatabase"].ConnectionString;

        private ISyncCsvService _syncService;

        private DwapiRemoteContext _context;

        private IClearCsvExtractsCommand _clearExtractsCommand;

        private ILoadPatientExtractCommand _loadPatientExtractCommand;
        private ILoadPatientArtExtractCommand _loadPatientArtExtractCommand;
        private ILoadPatientBaselinesExtractCommand _loadPatientBaselinesExtractCommand;
        private ILoadPatientLaboratoryExtractCommand _loadPatientLaboratoryExtractCommand;
        private ILoadPatientPharmacyExtractCommand _loadPatientPharmacyExtractCommand;
        private ILoadPatientStatusExtractCommand _loadPatientStatusExtractCommand;
        private ILoadPatientVisitExtractCommand _loadPatientVisitExtractCommand;

        private ILoadPatientExtractCsvCommand _loadPatientExtractCsvCommand;
        private ILoadPatientArtExtractCsvCommand _loadPatientArtExtractCsvCommand;
        private ILoadPatientBaselinesExtractCsvCommand _loadPatientBaselinesExtractCsvCommand;
        private ILoadPatientLaboratoryExtractCsvCommand _loadPatientLaboratoryExtractCsvCommand;
        private ILoadPatientPharmacyExtractCsvCommand _loadPatientPharmacyExtractCsvCommand;
        private ILoadPatientStatusExtractCsvCommand _loadPatientStatusExtractCsvCommand;
        private ILoadPatientVisitExtractCsvCommand _loadPatientVisitExtractCsvCommand;

        private IValidatePatientExtractCommand _validatePatientExtractCommand;
        private IValidatePatientArtExtractCommand _validatePatientArtExtractCommand;
        private IValidatePatientBaselinesExtractCommand _validatePatientBaselinesExtractCommand;
        private IValidatePatientLaboratoryExtractCommand _validatePatientLaboratoryExtractCommand;
        private IValidatePatientPharmacyExtractCommand _validatePatientPharmacyExtractCommand;
        private IValidatePatientStatusExtractCommand _validatePatientStatusExtractCommand;
        private IValidatePatientVisitExtractCommand _validatePatientVisitExtractCommand;

        private ISyncPatientExtractCommand _syncPatientExtractCommand;
        private ISyncPatientArtExtractCommand _syncPatientArtExtractCommand;
        private ISyncPatientBaselinesExtractCommand _syncPatientBaselinesExtractCommand;
        private Interfaces.Commands.ISyncPatientLaboratoryExtractCommand _syncPatientLaboratoryExtractCommand;
        private ISyncPatientPharmacyExtractCommand _syncPatientPharmacyExtractCommand;
        private ISyncPatientVisitExtractCommand _syncPatientVisitExtractCommand;
        private ISyncPatientStatusExtractCommand _syncPatientStatusExtractCommand;
        private int top = 10;
        private int topExtracts = -1;
        private string _csvARTPatientExtract;
        private string _csvPatientExtract;
        private string _csvPatientLaboratoryExtract;
        private string _csvPatientPharmacyExtract;
        private string _csvPatientStatusExtract;
        private string _csvPatientVisitExtract;
        private string _csvPatientWABWHOCD4Extract;

        [SetUp]
        public void should_SetUp()
        {
            _context = new DwapiRemoteContext();

            _clearExtractsCommand = new ClearCsvExtractsCommand(new EMRRepository(_context));

            _loadPatientExtractCsvCommand = new LoadPatientExtractCsvCommand(new EMRRepository(_context));
            _loadPatientArtExtractCsvCommand = new LoadPatientArtExtractCsvCommand(new EMRRepository(_context));
            _loadPatientBaselinesExtractCsvCommand = new LoadPatientBaselinesExtractCsvCommand(new EMRRepository(_context));
            _loadPatientLaboratoryExtractCsvCommand = new LoadPatientLaboratoryExtractCsvCommand(new EMRRepository(_context));
            _loadPatientPharmacyExtractCsvCommand = new LoadPatientPharmacyExtractCsvCommand(new EMRRepository(_context));
            _loadPatientVisitExtractCsvCommand = new LoadPatientVisitExtractCsvCommand(new EMRRepository(_context));
            _loadPatientStatusExtractCsvCommand = new LoadPatientStatusExtractCsvCommand(new EMRRepository(_context));

            _validatePatientExtractCommand = new ValidatePatientExtractCommand(new EMRRepository(_context), new ValidatorRepository(_context));
            _validatePatientArtExtractCommand = new ValidatePatientArtExtractCommand(new EMRRepository(_context), new ValidatorRepository(_context));
            _validatePatientBaselinesExtractCommand = new ValidatePatientBaselinesExtractCommand(new EMRRepository(_context), new ValidatorRepository(_context));
            _validatePatientLaboratoryExtractCommand = new ValidatePatientLaboratoryExtractCommand(new EMRRepository(_context), new ValidatorRepository(_context));
            _validatePatientPharmacyExtractCommand = new ValidatePatientPharmacyExtractCommand(new EMRRepository(_context), new ValidatorRepository(_context));
            _validatePatientVisitExtractCommand = new ValidatePatientVisitExtractCommand(new EMRRepository(_context), new ValidatorRepository(_context));
            _validatePatientStatusExtractCommand = new ValidatePatientStatusExtractCommand(new EMRRepository(_context), new ValidatorRepository(_context));

            _syncPatientExtractCommand = new SyncPatientExtractCommand(new EMRRepository(_context));
            _syncPatientArtExtractCommand = new SyncPatientArtExtractCommand(new EMRRepository(_context));
            _syncPatientBaselinesExtractCommand = new SyncPatientBaselinesExtractCommand(new EMRRepository(_context));
            _syncPatientLaboratoryExtractCommand = new SyncPatientLaboratoryExtractCommand(new EMRRepository(_context));
            _syncPatientPharmacyExtractCommand = new SyncPatientPharmacyExtractCommand(new EMRRepository(_context));
            _syncPatientVisitExtractCommand = new SyncPatientVisitExtractCommand(new EMRRepository(_context));
            _syncPatientStatusExtractCommand = new SyncPatientStatusExtractCommand(new EMRRepository(_context));

            _syncService = new SyncCsvService(
                _clearExtractsCommand,
                _loadPatientExtractCsvCommand, _loadPatientArtExtractCsvCommand, _loadPatientBaselinesExtractCsvCommand, _loadPatientLaboratoryExtractCsvCommand, _loadPatientPharmacyExtractCsvCommand, _loadPatientStatusExtractCsvCommand, _loadPatientVisitExtractCsvCommand,
                _validatePatientExtractCommand, _validatePatientArtExtractCommand, _validatePatientBaselinesExtractCommand, _validatePatientLaboratoryExtractCommand, _validatePatientPharmacyExtractCommand, _validatePatientStatusExtractCommand, _validatePatientVisitExtractCommand, _syncPatientExtractCommand, _syncPatientArtExtractCommand,
                _syncPatientBaselinesExtractCommand, _syncPatientLaboratoryExtractCommand, _syncPatientPharmacyExtractCommand, _syncPatientVisitExtractCommand, _syncPatientStatusExtractCommand);

            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientExtract;DELETE FROM  PatientExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientArtExtract;DELETE FROM  PatientArtExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientBaselinesExtract;DELETE FROM  PatientBaselinesExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientLaboratoryExtract;DELETE FROM  PatientLaboratoryExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientPharmacyExtract;DELETE FROM  PatientPharmacyExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientVisitExtract;DELETE FROM  PatientVisitExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientStatusExtract;DELETE FROM  PatientStatusExtract");

            _csvARTPatientExtract = TestHelpers.GetCsv("ARTPatientExtract","Extracts");
            _csvPatientExtract = TestHelpers.GetCsv("_PatientExtract", "Extracts");
            _csvPatientLaboratoryExtract = TestHelpers.GetCsv("PatientLaboratoryExtract", "Extracts");
            _csvPatientPharmacyExtract = TestHelpers.GetCsv("PatientPharmacyExtract", "Extracts");
            _csvPatientStatusExtract = TestHelpers.GetCsv("PatientStatusExtract", "Extracts");
            _csvPatientVisitExtract = TestHelpers.GetCsv("PatientVisitExtract", "Extracts");
            _csvPatientWABWHOCD4Extract = TestHelpers.GetCsv("PatientWABWHOCD4Extract", "Extracts");

            var ou= _syncService.InitializeAsync().Result;

            var pextractSetting = new ExtractSetting(nameof(TempPatientExtract));
            var psummary = _syncService.SyncAsync(pextractSetting, _csvPatientExtract).Result;
        }
        [Test]
        public void should_Sync_ARTPatientExtract()
        {
            var extractSetting=new ExtractSetting(nameof(TempPatientArtExtract));
            var summary= _syncService.SyncAsync(extractSetting, _csvARTPatientExtract).Result;
            Assert.IsTrue(_context.TempPatientArtExtracts.ToList().Count > 0);
            Console.WriteLine(summary);
        }
       
        [Test]
        public void should_Sync_PatientLaboratoryExtract()
        {
            var extractSetting = new ExtractSetting(nameof(TempPatientLaboratoryExtract));
            var summary = _syncService.SyncAsync(extractSetting, _csvARTPatientExtract).Result;
            Assert.IsTrue(_context.TempPatientLaboratoryExtracts.ToList().Count > 0);
            Console.WriteLine(summary);
        }
        [Test]
        public void should_Sync_PatientPharmacyExtract()
        {
            var extractSetting = new ExtractSetting(nameof(TempPatientPharmacyExtract));
            var summary = _syncService.SyncAsync(extractSetting, _csvPatientPharmacyExtract).Result;
            Assert.IsTrue(_context.TempPatientPharmacyExtracts.ToList().Count > 0);
            Console.WriteLine(summary);
        }
        [Test]
        public void should_Sync_PatientStatusExtract()
        {
            var extractSetting = new ExtractSetting(nameof(TempPatientStatusExtract));
            var summary = _syncService.SyncAsync(extractSetting, _csvPatientStatusExtract).Result;
            Assert.IsTrue(_context.TempPatientStatusExtracts.ToList().Count > 0);
            Console.WriteLine(summary);

        }
        [Test]
        public void should_Sync_PatientVisitExtract()
        {
            var extractSetting = new ExtractSetting(nameof(TempPatientVisitExtract));
            var summary = _syncService.SyncAsync(extractSetting, _csvPatientVisitExtract).Result;
            Assert.IsTrue(_context.TempPatientVisitExtracts.ToList().Count > 0);
            Console.WriteLine(summary);
        }
        [Test]
        public void should_Sync_PatientWABWHOCD4Extract()
        {
            var extractSetting = new ExtractSetting(nameof(TempPatientBaselinesExtract));
            var summary = _syncService.SyncAsync(extractSetting, _csvPatientWABWHOCD4Extract).Result;
            Assert.IsTrue(_context.TempPatientBaselinesExtracts.ToList().Count > 0);
            Console.WriteLine(summary);
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