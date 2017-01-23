using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Profiles;

namespace PalladiumDwh.Core.Tests.Model.Profiles
{
    [TestFixture]
    public class PatientStatusProfileTests
    {

        private List<PatientExtract> _patientExtracts;
        private Facility _facility;

        [SetUp]
        public void SetUp()
        {
            _facility = Builder<Facility>.CreateNew().Build();
            _patientExtracts = TestHelpers.GetTestPatientWithExtracts(_facility, 2, 10).ToList();
        }

        [Test]
        public void should_Create_PatientStatusProfile()
        {
            var patient = _patientExtracts.First();
            var artProfile = PatientStatusProfile.Create(_facility, patient);

            Assert.IsNotNull(artProfile.Demographic);
            Assert.IsNotNull(artProfile.Facility);
            Assert.That(artProfile.StatusExtracts.Count, Is.EqualTo(10));
            Assert.IsNull(artProfile.PatientStatusExtracts);
        }

        [Test]
        public void should_CheckValidity()
        {
            var patient = _patientExtracts.First();

            
            var artProfile = PatientStatusProfile.Create(_facility, patient);
            Assert.IsTrue(artProfile.IsValid());

            var nullProfile = PatientStatusProfile.Create(_facility, patient);
            nullProfile.Facility = null;
            nullProfile.Demographic = null;
            nullProfile.StatusExtracts = null;
            Assert.IsFalse(nullProfile.IsValid());

            var noPatientProfile = PatientStatusProfile.Create(_facility, patient);
            noPatientProfile.Demographic = null;
            Assert.IsFalse(noPatientProfile.IsValid());

            var noFacilityProfile = PatientStatusProfile.Create(_facility, patient);
            noFacilityProfile.Facility = null;
            Assert.IsFalse(noFacilityProfile.IsValid());

            var noExtractsProfile = PatientStatusProfile.Create(_facility, patient);
            noExtractsProfile.StatusExtracts = null;
            Assert.IsFalse(noExtractsProfile.IsValid());

            noExtractsProfile = PatientStatusProfile.Create(_facility, patient);
            noExtractsProfile.StatusExtracts = new List<PatientStatusExtractDTO>();
            Assert.IsFalse(noExtractsProfile.IsValid());
        }

        [Test]
        public void should_GeneratePatientRecord()
        {
            var patient = _patientExtracts.First();
            var artProfile = PatientStatusProfile.Create(_facility, patient);

            artProfile.GeneratePatientRecord();

            Assert.IsNotNull(artProfile.FacilityInfo);
            Assert.IsNotNull(artProfile.PatientInfo);
            Assert.That(artProfile.StatusExtracts.Count, Is.EqualTo(10));
            Assert.IsNull(artProfile.PatientStatusExtracts);
        }

        [Test]
        public void should_GenerateRecords()
        {
            var patient = _patientExtracts.First();
            var artProfile = PatientStatusProfile.Create(_facility, patient);
            artProfile.GeneratePatientRecord();

            artProfile.GenerateRecords(patient.Id);

            Assert.That(artProfile.PatientStatusExtracts.Count, Is.EqualTo(10));
            Assert.AreEqual(artProfile.PatientInfo.Id, artProfile.PatientStatusExtracts.First().PatientId);
        }
    }
}