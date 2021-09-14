using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using NUnit.Framework;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Services;
using PalladiumDwh.DWapi.Controllers;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Infrastructure.Data.Repository;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.DWapi.Tests
{
    [TestFixture]
    public class FacilitiesControllerTests
    {
        private static readonly string baseUrl = "http://localhost/api/Facilities";

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
        private IPatientAdverseEventRepository _patientAdverseEventRepository;

        private FacilitiesController _facilitiesController;


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
                _patientVisitRepository = new PatientVisitRepository(_context),
                _patientAdverseEventRepository=new PatientAdverseEventRepository(_context),
                null,null,null,null,null,null,null,null,null,null,
                null,null,new ActionRegisterRepository(_context)
            );

            _facilitiesController = new FacilitiesController(_syncService);
            TestHelpers.SetupControllerForTests(_facilitiesController,baseUrl, "Facilities");
        }

        [Test]
        public void should_Get()
        {
            var facility = _facilities.First();


            var result = _facilitiesController.Get(facility.Code);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            var savedFacility = result.Content.ReadAsAsync<Facility>().Result;

            Assert.IsNotNull(savedFacility);
            Assert.AreEqual(savedFacility.Code,facility.Code);
            Assert.AreEqual(savedFacility.Name, facility.Name);
        }
    }
}
