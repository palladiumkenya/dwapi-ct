using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using NUnit.Framework;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Core.Model.Profiles;
using PalladiumDwh.Core.Services;
using PalladiumDwh.DWapi.Controllers;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Infrastructure.Data.Repository;

namespace PalladiumDwh.DWapi.Tests
{
    [TestFixture]
    public class PatientLabsControllerTests
    {
        private static readonly string baseUrl = "http://localhost/api/PatientLabs";

        private ISyncService _syncService;
        private DwapiCentralContext _context;
        private List<Facility> _facilities;
        private IFacilityRepository _facilityRepository;
        private IPatientExtractRepository _patientExtractRepository;
        private IPatientArtExtractRepository _patientArtExtractRepository;
        private IPatientBaseLinesRepository _patientBaseLinesRepository;
        private IPatientLabRepository _patientLabRepository;
        private IPatientPharmacyRepository _patientPharmacyRepository;

        private IPatientStatusRepository _patientStatusRepository;
        private IPatientVisitRepository _patientVisitRepository;

        private PatientLabsController _controller;
        private List<PatientExtract> _patientWithAllExtracts;
        private Facility _facility;


        [SetUp]
        public void SetUp()
        {
            _context = new DwapiCentralContext(Effort.DbConnectionFactory.CreateTransient(), true);
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
                _patientVisitRepository = new PatientVisitRepository(_context)
            );

            _controller = new PatientLabsController(_syncService);
            TestHelpers.SetupControllerForTests(_controller, baseUrl, "PatientLabs");

            _facility = _facilities.First();
            _patientWithAllExtracts = TestHelpers.GetTestPatientWithExtracts(_facility, 2, 10).ToList();
        }

        [Test]
        public void should_Post()
        {
            var patient = _patientWithAllExtracts.First();
            var profile = PatientLabProfile.Create(_facility, patient);

            var result = _controller.Post(profile);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            var savedPatient = _patientExtractRepository.Find(profile.PatientInfo.Id);
            Assert.IsNotNull(savedPatient);
            Assert.IsTrue(savedPatient.PatientLaboratoryExtracts.Count > 0);

        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}