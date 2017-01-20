using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Core.Model.DTO;
using PalladiumDwh.Core.Model.Profiles;

namespace PalladiumDwh.Core.Tests.Model.Profiles
{
    [TestFixture]
    public class PatientVisitProfileTests
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
        public void should_Create_PatientVisitProfile()
        {
            var patient = _patientExtracts.First();
            var profile = PatientVisitProfile.Create(_facility, patient);

            Assert.IsNotNull(profile.Demographic);
            Assert.IsNotNull(profile.Facility);
            Assert.That(profile.VisitExtracts.Count, Is.EqualTo(10));
            Assert.IsNull(profile.PatientVisitExtracts);
        }

        [Test]
        public void should_CheckValidity()
        {
            var patient = _patientExtracts.First();


            var profile = PatientVisitProfile.Create(_facility, patient);
            Assert.IsTrue(profile.IsValid());

            var nullProfile = PatientVisitProfile.Create(_facility, patient);
            nullProfile.Facility = null;
            nullProfile.Demographic = null;
            nullProfile.VisitExtracts = null;
            Assert.IsFalse(nullProfile.IsValid());

            var noPatientProfile = PatientVisitProfile.Create(_facility, patient);
            noPatientProfile.Demographic = null;
            Assert.IsFalse(noPatientProfile.IsValid());

            var noFacilityProfile = PatientVisitProfile.Create(_facility, patient);
            noFacilityProfile.Facility = null;
            Assert.IsFalse(noFacilityProfile.IsValid());

            var noExtractsProfile = PatientVisitProfile.Create(_facility, patient);
            noExtractsProfile.VisitExtracts = null;
            Assert.IsFalse(noExtractsProfile.IsValid());

            noExtractsProfile = PatientVisitProfile.Create(_facility, patient);
            noExtractsProfile.VisitExtracts = new List<PatientVisitExtractDTO>();
            Assert.IsFalse(noExtractsProfile.IsValid());
        }

        [Test]
        public void should_GeneratePatientRecord()
        {
            var patient = _patientExtracts.First();
            var profile = PatientVisitProfile.Create(_facility, patient);

            profile.GeneratePatientRecord();

            Assert.IsNotNull(profile.FacilityInfo);
            Assert.IsNotNull(profile.PatientInfo);
            Assert.That(profile.VisitExtracts.Count, Is.EqualTo(10));
            Assert.IsNull(profile.PatientVisitExtracts);
        }

        [Test]
        public void should_GenerateRecords()
        {
            var patient = _patientExtracts.First();
            var profile = PatientVisitProfile.Create(_facility, patient);
            profile.GeneratePatientRecord();

            profile.GenerateRecords(patient.Id);

            Assert.That(profile.PatientVisitExtracts.Count, Is.EqualTo(10));
            Assert.AreEqual(profile.PatientInfo.Id, profile.PatientVisitExtracts.First().PatientId);
        }
    }
}