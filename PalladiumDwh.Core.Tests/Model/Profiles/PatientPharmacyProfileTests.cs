using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Tests.Model.Profiles
{
    [TestFixture]
    public class PatientPharmacyProfileTests
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
        public void should_Create_PatientPharmacyProfile()
        {
            var patient = _patientExtracts.First();
            var profile = PatientPharmacyProfile.Create(_facility, patient);

            Assert.IsNotNull(profile.Demographic);
            Assert.IsNotNull(profile.Facility);
            Assert.That(profile.PharmacyExtracts.Count, Is.EqualTo(10));
            Assert.IsNull(profile.Extracts);
        }

        [Test]
        public void should_CheckValidity()
        {
            var patient = _patientExtracts.First();


            var profile = PatientPharmacyProfile.Create(_facility, patient);
            Assert.IsTrue(profile.IsValid());

            var nullProfile = PatientPharmacyProfile.Create(_facility, patient);
            nullProfile.Facility = null;
            nullProfile.Demographic = null;
            nullProfile.PharmacyExtracts = null;
            Assert.IsFalse(nullProfile.IsValid());

            var noPatientProfile = PatientPharmacyProfile.Create(_facility, patient);
            noPatientProfile.Demographic = null;
            Assert.IsFalse(noPatientProfile.IsValid());

            var noFacilityProfile = PatientPharmacyProfile.Create(_facility, patient);
            noFacilityProfile.Facility = null;
            Assert.IsFalse(noFacilityProfile.IsValid());

            var noExtractsProfile = PatientPharmacyProfile.Create(_facility, patient);
            noExtractsProfile.PharmacyExtracts = null;
            Assert.IsFalse(noExtractsProfile.IsValid());

            noExtractsProfile = PatientPharmacyProfile.Create(_facility, patient);
            noExtractsProfile.PharmacyExtracts = new List<PatientPharmacyExtractDTO>();
            Assert.IsFalse(noExtractsProfile.IsValid());
        }

        [Test]
        public void should_GeneratePatientRecord()
        {
            var patient = _patientExtracts.First();
            var profile = PatientPharmacyProfile.Create(_facility, patient);

            profile.GeneratePatientRecord();

            Assert.IsNotNull(profile.FacilityInfo);
            Assert.IsNotNull(profile.PatientInfo);
            Assert.That(profile.PharmacyExtracts.Count, Is.EqualTo(10));
            Assert.IsNull(profile.Extracts);
        }

        [Test]
        public void should_GenerateRecords()
        {
            var patient = _patientExtracts.First();
            var profile = PatientPharmacyProfile.Create(_facility, patient);
            profile.GeneratePatientRecord();

            profile.GenerateRecords(patient.Id);

            Assert.That(profile.Extracts.Count, Is.EqualTo(10));
            Assert.AreEqual(profile.PatientInfo.Id, profile.Extracts.First().PatientId);
        }
    }
}