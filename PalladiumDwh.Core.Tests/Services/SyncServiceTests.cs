using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Core.Model.Profiles;
using PalladiumDwh.Core.Services;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Infrastructure.Data.Repository;

namespace PalladiumDwh.Core.Tests.Services
{
    [TestFixture]
    public class SyncServiceTests
    {
        private ISyncService _syncService;
        private Facility _newFacility;
        private List<PatientExtract> _patientExtracts;
        private DwapiCentralContext _context;
        private List<Facility> _facilities;
        private IFacilityRepository _facilityRepository;
        private IPatientExtractRepository _patientExtractRepository;


        [SetUp]
        public void SetUp()
        {
            _context = new DwapiCentralContext(Effort.DbConnectionFactory.CreateTransient(), true);

            _facilities = TestHelpers.GetTestData<Facility>(5).ToList();
            TestHelpers.CreateTestData(_context, _facilities);

            _syncService = new SyncService(
                _facilityRepository = new FacilityRepository(_context),
                _patientExtractRepository = new PatientExtractRepository(_context),
                new PatientArtExtractRepository(_context),
                new PatientBaseLinesRepository(_context),
                new PatientLabRepository(_context),
                new PatientPharmacyRepository(_context),
                new PatientStatusRepository(_context),
                new PatientVisitRepository(_context)
            );

            _newFacility = Builder<Facility>.CreateNew().Build();
            _patientExtracts = TestHelpers.GetTestPatientArtData(_newFacility, 2, 10).ToList();
        }

        [Test]
        public void should_Get_Facility()
        {
            var facility = _syncService.GetFacility(_facilities.First().Code);
            Assert.IsNotNull(facility);
        }
        [Test]
        public void should_SynArt_new()
        {
            var patient = _patientExtracts.First();
            var artProfile = PatientARTProfile.Create(_newFacility, patient);

            _syncService.SyncArt(artProfile);

            var facility = _syncService.GetFacility(_newFacility.Code);
            Assert.IsNotNull(facility);

            var savedPatient = _patientExtractRepository.Find(artProfile.PatientInfo.Id);
            Assert.IsNotNull(savedPatient);
            Assert.IsTrue(savedPatient.PatientArtExtracts.Count>0);
        }
   
    }
}