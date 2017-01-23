using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Infrastructure.Data.Repository;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Infrastructure.Tests
{
    [TestFixture]
    public class PatientExtractRepositoryTests 
    {
        private DwapiCentralContext _context;
        private List<Facility> _facilities;
        private IPatientExtractRepository _patientExtractRepository;
        private Facility _facilityA;
        private List<PatientExtract> _patients;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiCentralContext(Effort.DbConnectionFactory.CreateTransient(), true);
            _facilities = TestHelpers.GetTestData<Facility>(5).ToList();
            TestHelpers.CreateTestData(_context, _facilities);

            _facilityA = _facilities.First();
            _patients = TestHelpers.GetTestPatientData(_facilityA, 2, 10).ToList();
            TestHelpers.CreateTestData(_context, _patients);

            _patientExtractRepository = new PatientExtractRepository(_context);
        }

      
        [Test]
        public void should_Sync_New()
        {
            var patientExtract = Builder<PatientExtract>.CreateNew().Build();
            patientExtract.PatientPID = DateTime.UtcNow.Millisecond;
            patientExtract.FacilityId = _facilityA.Id;
            Console.WriteLine(patientExtract.PatientPID);

            _patientExtractRepository.Sync(patientExtract);

            var saved = _patientExtractRepository.Find(patientExtract.Id);
            Assert.IsNotNull(saved);
            Assert.AreEqual(saved.PatientPID, patientExtract.PatientPID);
            Console.WriteLine(saved.PatientPID);
        }

        [Test]
        public void should_Sync_Exisitng()
        {
            var patientExtract = _patients.Last();
            Console.WriteLine(patientExtract.PatientPID);

            var newPatientExtract = Builder<PatientExtract>.CreateNew().Build();
            newPatientExtract.PatientPID = patientExtract.PatientPID;
            newPatientExtract.FacilityId = patientExtract.FacilityId;
            Console.WriteLine(newPatientExtract.PatientPID);

            _patientExtractRepository.Sync(newPatientExtract);

            var saved = _patientExtractRepository.Find(newPatientExtract.Id);
            Assert.IsNotNull(saved);
            Assert.AreEqual(saved.PatientPID, newPatientExtract.PatientPID);
            Assert.AreEqual(saved.Id, newPatientExtract.Id);
            Console.WriteLine(saved.PatientPID);
        }
    }
}
