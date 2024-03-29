﻿using System;
using System.Configuration;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.ClientReader.Core.Services;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.ClientReader.Infrastructure.Data.Command;
using PalladiumDwh.ClientReader.Infrastructure.Data.Repository;
using PalladiumDwh.Shared.Model;


namespace PalladiumDwh.ClientReader.Core.Tests.Services
{
    [TestFixture]
    public class SyncServiceTests
    {
        private readonly string _cn = ConfigurationManager.ConnectionStrings["DWAPIRemote"].ConnectionString;
        private readonly string _srcCn = ConfigurationManager.ConnectionStrings["EMRDatabase"].ConnectionString;

        private ISyncService _syncService;

        private DwapiRemoteContext _context;

        private IAnalyzeTempExtractsCommand _analyzeTempExtractsCommand;

        private IClearExtractsCommand _clearExtractsCommand;
        private ILoadPatientExtractCommand _loadPatientExtractCommand;
        private ILoadPatientArtExtractCommand _loadPatientArtExtractCommand;
        private ILoadPatientBaselinesExtractCommand _loadPatientBaselinesExtractCommand;
        private ILoadPatientLaboratoryExtractCommand _loadPatientLaboratoryExtractCommand;
        private ILoadPatientPharmacyExtractCommand _loadPatientPharmacyExtractCommand;
        private ILoadPatientStatusExtractCommand _loadPatientStatusExtractCommand;
        private ILoadPatientVisitExtractCommand _loadPatientVisitExtractCommand;

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
        private IProgress<DProgress> _dprogress;

        [SetUp]
        public void should_SetUp()
        {

            _dprogress = new Progress<DProgress>(ReportDProgress);

            _context = new DwapiRemoteContext();

            _analyzeTempExtractsCommand=new AnalyzeTempExtractsCommand(new EMRRepository(_context),new DatabaseManager(_context));
            _clearExtractsCommand =new ClearExtractsCommand(new EMRRepository(_context));

            _loadPatientExtractCommand = new LoadPatientExtractCommand(new EMRRepository(_context));
            _loadPatientArtExtractCommand = new LoadPatientArtExtractCommand(new EMRRepository(_context));
            _loadPatientBaselinesExtractCommand = new LoadPatientBaselinesExtractCommand(new EMRRepository(_context));
            _loadPatientLaboratoryExtractCommand = new LoadPatientLaboratoryExtractCommand(new EMRRepository(_context));
            _loadPatientPharmacyExtractCommand = new LoadPatientPharmacyExtractCommand(new EMRRepository(_context));
            _loadPatientVisitExtractCommand = new LoadPatientVisitExtractCommand(new EMRRepository(_context));
            _loadPatientStatusExtractCommand = new LoadPatientStatusExtractCommand(new EMRRepository(_context));

            _validatePatientExtractCommand = new ValidatePatientExtractCommand(new EMRRepository(_context),new ValidatorRepository(_context));
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

            _syncService = new SyncService(
            _analyzeTempExtractsCommand,
        _clearExtractsCommand,
                _loadPatientExtractCommand, _loadPatientArtExtractCommand, _loadPatientBaselinesExtractCommand,_loadPatientLaboratoryExtractCommand, _loadPatientPharmacyExtractCommand,_loadPatientStatusExtractCommand, _loadPatientVisitExtractCommand,
                _validatePatientExtractCommand,_validatePatientArtExtractCommand,_validatePatientBaselinesExtractCommand,_validatePatientLaboratoryExtractCommand,_validatePatientPharmacyExtractCommand,_validatePatientStatusExtractCommand,_validatePatientVisitExtractCommand,_syncPatientExtractCommand, _syncPatientArtExtractCommand, 
                _syncPatientBaselinesExtractCommand,_syncPatientLaboratoryExtractCommand, _syncPatientPharmacyExtractCommand,_syncPatientVisitExtractCommand, _syncPatientStatusExtractCommand);


            _context.Database.ExecuteSqlCommand("DELETE FROM EventHistory");
            _context.Database.ExecuteSqlCommand("DELETE FROM ValidationError");

            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientExtract;DELETE FROM  PatientExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientArtExtract;DELETE FROM  PatientArtExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientBaselinesExtract;DELETE FROM  PatientBaselinesExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientLaboratoryExtract;DELETE FROM  PatientLaboratoryExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientPharmacyExtract;DELETE FROM  PatientPharmacyExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientVisitExtract;DELETE FROM  PatientVisitExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientStatusExtract;DELETE FROM  PatientStatusExtract");
        }


        [Test]
        public void should_InitializeAsync()
        {
            var result = _syncService.InitializeAsync(_dprogress).Result;
            Assert.IsTrue(result==1);
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
            _syncService.Sync(new ExtractSetting(nameof(TempPatientExtract)));

            var summary=_syncService.Sync(new ExtractSetting(extract));
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
        public void should_SyncPatientsAsync()
        {
            var summary = _syncService.SyncPatientsAsync().Result;
            var cleanRecords = summary.LoadSummary.Total - summary.ValidationSummary.Total;
            
            Assert.AreEqual(summary.LoadSummary.Total, _context.TempPatientExtracts.Count());
            Assert.AreEqual(summary.ValidationSummary.Total, _context.ValidationErrors.Count());
            Assert.AreEqual(summary.SyncSummary.Total, _context.ClientPatientExtracts.Count());
            Assert.AreEqual(cleanRecords, summary.SyncSummary.Total);
            Console.WriteLine(summary.Report());
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
            _context.Database.ExecuteSqlCommand("DELETE FROM EventHistory");
            _context.Database.ExecuteSqlCommand("DELETE FROM ValidationError");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientExtract;DELETE FROM  PatientExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientArtExtract;DELETE FROM  PatientArtExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientBaselinesExtract;DELETE FROM  PatientBaselinesExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientLaboratoryExtract;DELETE FROM  PatientLaboratoryExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientPharmacyExtract;DELETE FROM  PatientPharmacyExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientStatusExtract;DELETE FROM  PatientStatusExtract");
            _context.Database.ExecuteSqlCommand("DELETE FROM TempPatientVisitExtract;DELETE FROM  PatientVisitExtract");

        }
        private void ReportDProgress(DProgress value)
        {
            Console.WriteLine(value);
        }
    }
}