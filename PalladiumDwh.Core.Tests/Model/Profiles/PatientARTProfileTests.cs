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
    public class PatientARTProfileTests
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
        public void should_Create_PatientARTProfile()
        {
            var patient = _patientExtracts.First();
            var artProfile = PatientARTProfile.Create(_facility, patient);

            Assert.IsNotNull(artProfile.Demographic);
            Assert.IsNotNull(artProfile.Facility);
            Assert.That(artProfile.ArtExtracts.Count, Is.EqualTo(10));
            Assert.IsNull(artProfile.PatientArtExtracts);
        }

        [Test]
        public void should_CheckValidity()
        {
            var patient = _patientExtracts.First();

            
            var artProfile = PatientARTProfile.Create(_facility, patient);
            Assert.IsTrue(artProfile.IsValid());

            var nullProfile = PatientARTProfile.Create(_facility, patient);
            nullProfile.Facility = null;
            nullProfile.Demographic = null;
            nullProfile.ArtExtracts = null;
            Assert.IsFalse(nullProfile.IsValid());

            var noPatientProfile = PatientARTProfile.Create(_facility, patient);
            noPatientProfile.Demographic = null;
            Assert.IsFalse(noPatientProfile.IsValid());

            var noFacilityProfile = PatientARTProfile.Create(_facility, patient);
            noFacilityProfile.Facility = null;
            Assert.IsFalse(noFacilityProfile.IsValid());

            var noExtractsProfile = PatientARTProfile.Create(_facility, patient);
            noExtractsProfile.ArtExtracts = null;
            Assert.IsFalse(noExtractsProfile.IsValid());

            noExtractsProfile = PatientARTProfile.Create(_facility, patient);
            noExtractsProfile.ArtExtracts = new List<PatientArtExtractDTO>();
            Assert.IsFalse(noExtractsProfile.IsValid());
        }

        [Test]
        public void should_GeneratePatientRecord()
        {
            var patient = _patientExtracts.First();
            var artProfile = PatientARTProfile.Create(_facility, patient);

            artProfile.GeneratePatientRecord();

            Assert.IsNotNull(artProfile.FacilityInfo);
            Assert.IsNotNull(artProfile.PatientInfo);
            Assert.That(artProfile.ArtExtracts.Count, Is.EqualTo(10));
            Assert.IsNull(artProfile.PatientArtExtracts);
        }

        [Test]
        public void should_GenerateRecords()
        {
            var patient = _patientExtracts.First();
            var artProfile = PatientARTProfile.Create(_facility, patient);
            artProfile.GeneratePatientRecord();

            artProfile.GenerateRecords(patient.Id);

            Assert.That(artProfile.PatientArtExtracts.Count, Is.EqualTo(10));
            Assert.AreEqual(artProfile.PatientInfo.Id, artProfile.PatientArtExtracts.First().PatientId);
        }
    }
}