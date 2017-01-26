using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data
{
    [TestFixture]
    public class PatientExtractRepositoryTests
    {
        private DwapiRemoteContext _context;
        private List<PatientExtract> _patientExtracts;
        private IPatientExtractRepository _patientExtractRepository;
        private Guid _facilityId;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext(Effort.DbConnectionFactory.CreateTransient(), true);
            var facilities = Builder<Facility>.CreateListOfSize(1).Build();
            TestHelpers.CreateTestData(_context, facilities);
            _facilityId = facilities.First().Id;
            _patientExtracts = TestHelpers.GetTestData<PatientExtract>(5).ToList();
            foreach (var p in _patientExtracts)
            {
                p.FacilityId = _facilityId;
            }
            TestHelpers.CreateTestData(_context, _patientExtracts);

            _patientExtractRepository=new PatientExtractRepository(_context);
        }

        [Test]
        public void should_GetPatientBy_facility_patientPK()
        {
            var pid = _patientExtracts.Last().PatientPID;

            var patient=_patientExtractRepository.GetPatientBy(_facilityId,pid);

            Assert.IsNotNull(patient);
        }

     
        [Test]
        public void should_Sync()
        {
            var newPatientExtracts = Builder<PatientExtract>.CreateListOfSize(3).Build().ToList();
            foreach (var p in newPatientExtracts)
            {
                p.FacilityId = _facilityId;
            }
            var newIds = newPatientExtracts.Select(x => x.Id).ToList();


            _patientExtractRepository.Sync(newPatientExtracts);
            

            var allsavedNew = _patientExtractRepository.GetAll().Where(x => newIds.Contains(x.Id)).ToList();
            Assert.IsTrue(allsavedNew.Count==3);
        }
    }
}