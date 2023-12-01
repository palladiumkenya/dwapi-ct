using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Services;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Infrastructure.Data.Repository;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Tests.Services
{
    [TestFixture]
    public class SyncServiceTests
    {
        private ISyncService _syncService;
        private Facility _newFacility;
        private DwapiCentralContext _dbcontext;
        private DwapiCentralContext _context;
        private List<Facility> _facilities;
        private IFacilityRepository _facilityRepository;
        private IPatientExtractRepository _patientExtractRepository;
        private IPatientArtExtractRepository _patientArtExtractRepository;
        private IPatientBaseLinesRepository _patientBaseLinesRepository;
        private IPatientLabRepository _patientLabRepository;
        private IPatientPharmacyRepository _patientPharmacyRepository;
        private IPatientAdverseEventRepository _patientAdverseEventRepository;

        private IPatientStatusRepository _patientStatusRepository;
        private IPatientVisitRepository _patientVisitRepository;
        private List<PatientExtract> _patientWithAllExtracts;
        private List<PatientExtract> _manifestPatientWithAllExtracts;
        private Facility _manifestFacility;

        private Facility _facilityA;
        private List<PatientExtract> _patients;

        [SetUp]
        public void SetUp()
        {
            _dbcontext = TestInitializer.Container.GetInstance<DwapiCentralContext>(); 
            _facilityA =new Facility(999999,"FTest","E1","P1");
            _dbcontext.Database.ExecuteSqlCommand($@"DELETE FROM [Facility] where Code={_facilityA.Code}");
            TestHelpers.CreateTestData(_dbcontext, new List<Facility> { _facilityA });
            _patients = TestHelpers.GetTestPatientWithExtracts(_facilityA, 5, 1).ToList();
            int pid = 1000;
            foreach (var p in _patients)
            {
                p.PatientPID = pid;
                pid++;
            }
            TestHelpers.CreateTestData(_dbcontext, _patients);


            _context = _dbcontext; // new DwapiCentralContext(Effort.DbConnectionFactory.CreateTransient(), true);
            _facilities = TestHelpers.GetTestData<Facility>(5).ToList();
            TestHelpers.CreateTestData(_context, _facilities);

            _syncService = new SyncService(
                _facilityRepository = new FacilityRepository(_context),
                _patientExtractRepository = new PatientExtractRepository(_context),
                _patientArtExtractRepository = new PatientArtExtractRepository(_context),
                _patientBaseLinesRepository = new PatientBaseLinesRepository(_context),
                _patientLabRepository = new PatientLabRepository(_context),
                _patientPharmacyRepository = new PatientPharmacyRepository(_context),
                _patientStatusRepository = new PatientStatusRepository(_context),
                _patientVisitRepository = new PatientVisitRepository(_context),
                _patientAdverseEventRepository=new PatientAdverseEventRepository(_context),
                null,null,null,null,null,null,null,null,null,null,
                null,null,
                new ActionRegisterRepository(_context)
            );

            _newFacility = Builder<Facility>.CreateNew().Build();
            _patientWithAllExtracts = TestHelpers.GetTestPatientWithExtracts(_newFacility, 2, 10).ToList();

            _manifestFacility= _facilities.First();
            _manifestPatientWithAllExtracts = TestHelpers.GetTestPatientWithExtracts(_manifestFacility, 4, 1).ToList();

            int id = _patientWithAllExtracts.Max(x => x.PatientPID);
            foreach (var p in _manifestPatientWithAllExtracts)
            {
                id++;
                p.PatientPID = id;
            }

            TestHelpers.CreateTestData(_context,_manifestPatientWithAllExtracts);
        }

        [Test]
        public void should_Get_Facility()
        {
            var facility = _syncService.GetFacility(_facilities.First().Code);
            Assert.IsNotNull(facility);
        }

        [Test]
        public void should_Sync_Patient()
        {
            var _patientExtracts = TestHelpers.GetTestPatientData(_newFacility, 2).ToList();
            var patient = _patientExtracts.First();
            var patientProfile = PatientProfile.Create(_newFacility, patient);
            patientProfile.GeneratePatientRecord();

            var id=_syncService.SyncPatient(patientProfile);
            Assert.IsNotNull(id);
            Assert.IsTrue(id!=Guid.Empty);

            var savedPatient = _patientExtractRepository.Find(id.Value);
            Assert.IsNotNull(savedPatient);

            var facility = _facilityRepository.Find(savedPatient.FacilityId);
            Assert.IsNotNull(facility);
            Assert.AreEqual(_newFacility.Code,facility.Code);
        }

        [Test]
        public void should_SynManifest()
        {
            _syncService = new SyncService(
                _facilityRepository = new FacilityRepository(_context),
                _patientExtractRepository = new PatientExtractRepository(_dbcontext),
                _patientArtExtractRepository = new PatientArtExtractRepository(_context),
                _patientBaseLinesRepository = new PatientBaseLinesRepository(_context),
                _patientLabRepository = new PatientLabRepository(_context),
                _patientPharmacyRepository = new PatientPharmacyRepository(_context),
                _patientStatusRepository = new PatientStatusRepository(_context),
                _patientVisitRepository = new PatientVisitRepository(_context),
                _patientAdverseEventRepository=new PatientAdverseEventRepository(_context),
                null,null,null,null,null,null,null,null,null,
                null,null,null,
                new ActionRegisterRepository(_context)
            );

            var manifest = new Manifest(_facilityA.Code);
            var currentPatients = _patients.Where(x => x.PatientPID > 1000);
            foreach (var p in currentPatients)
            {
                manifest.AddPatientPk(p.PatientPID);
            }

            _syncService.SyncManifest(manifest);

            _patientExtractRepository = new PatientExtractRepository(new DwapiCentralContext());
            var cleanPatients = _patientExtractRepository.GetAllBy(x => x.FacilityId == _facilityA.Id).ToList();
            Assert.IsTrue(cleanPatients.Count == 4);
        }

        [Test]
        public void should_SynArt()
        {
            var patient = _patientWithAllExtracts.First();
            var profile = PatientARTProfile.Create(_newFacility, patient);

            _syncService.SyncArt(profile);

            var savedPatient = _patientExtractRepository.Find(profile.PatientInfo.Id);
            Assert.IsNotNull(savedPatient);
            Assert.IsTrue(savedPatient.PatientArtExtracts.Count>0);
        }


        [Test]
        public void should_SynBaseline()
        {
            var patient = _patientWithAllExtracts.First();
            var profile = PatientBaselineProfile.Create(_newFacility, patient);

            _syncService.SyncBaseline(profile);

            var savedPatient = _patientExtractRepository.Find(profile.PatientInfo.Id);
            Assert.IsNotNull(savedPatient);
            Assert.IsTrue(savedPatient.PatientBaselinesExtracts.Count > 0);
        }
        [Test]
        public void should_SynLab()
        {
            var patient = _patientWithAllExtracts.First();
            var profile = PatientLabProfile.Create(_newFacility, patient);

            _syncService.SyncLab(profile);

            var savedPatient = _patientExtractRepository.Find(profile.PatientInfo.Id);
            Assert.IsNotNull(savedPatient);
            Assert.IsTrue(savedPatient.PatientLaboratoryExtracts.Count > 0);
        }
        [Test]
        public void should_SyncPharmacy()
        {
            var patient = _patientWithAllExtracts.First();
            var profile = PatientPharmacyProfile.Create(_newFacility, patient);

            _syncService.SyncPharmacy(profile);

            var savedPatient = _patientExtractRepository.Find(profile.PatientInfo.Id);
            Assert.IsNotNull(savedPatient);
            Assert.IsTrue(savedPatient.PatientPharmacyExtracts.Count > 0);
        }
        [Test]
        public void should_SyncStatus()
        {
            var patient = _patientWithAllExtracts.First();
            var profile = PatientStatusProfile.Create(_newFacility, patient);

            _syncService.SyncStatus(profile);

            var savedPatient = _patientExtractRepository.Find(profile.PatientInfo.Id);
            Assert.IsNotNull(savedPatient);
            Assert.IsTrue(savedPatient.PatientStatusExtracts.Count > 0);
        }
        [Test]
        public void should_SyncVisits()
        {
            var patient = _patientWithAllExtracts.First();
            var profile = PatientVisitProfile.Create(_newFacility, patient);

            _syncService.SyncVisit(profile);

            var savedPatient = _patientExtractRepository.Find(profile.PatientInfo.Id);
            Assert.IsNotNull(savedPatient);
            Assert.IsTrue(savedPatient.PatientVisitExtracts.Count > 0);
        }

        [Test]
        public void should_SyncAdeversEvents()
        {
            var patient = _patientWithAllExtracts.First();
            var profile = PatientAdverseEventProfile.Create(_newFacility, patient);

            _syncService.SyncAdverseEvent(profile);

            var savedPatient = _patientExtractRepository.Find(profile.PatientInfo.Id);
            Assert.IsNotNull(savedPatient);
            Assert.IsTrue(savedPatient.PatientAdverseEventExtracts.Count > 0);
        }
        [TearDown]
        public void TearDown()
        {
          //   _dbcontext = new DwapiCentralContext();
            _dbcontext.Database.ExecuteSqlCommand($@"DELETE FROM [Facility] where Code={_facilityA.Code}");
        }

    }
}
