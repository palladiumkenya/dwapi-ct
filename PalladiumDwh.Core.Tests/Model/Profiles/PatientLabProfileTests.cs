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
    public class PatientLabProfileTests
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
        public void should_Create_PatientLabProfile()
        {
            var patient = _patientExtracts.First();
            var artProfile = PatientLabProfile.Create(_facility, patient);

            Assert.IsNotNull(artProfile.Demographic);
            Assert.IsNotNull(artProfile.Facility);
            Assert.That(artProfile.LaboratoryExtracts.Count, Is.EqualTo(10));
            Assert.IsNull(artProfile.Extracts);
        }

        [Test]
        public void should_CheckValidity()
        {
            var patient = _patientExtracts.First();

            
            var artProfile = PatientLabProfile.Create(_facility, patient);
            Assert.IsTrue(artProfile.IsValid());

            var nullProfile = PatientLabProfile.Create(_facility, patient);
            nullProfile.Facility = null;
            nullProfile.Demographic = null;
            nullProfile.LaboratoryExtracts = null;
            Assert.IsFalse(nullProfile.IsValid());

            var noPatientProfile = PatientLabProfile.Create(_facility, patient);
            noPatientProfile.Demographic = null;
            Assert.IsFalse(noPatientProfile.IsValid());

            var noFacilityProfile = PatientLabProfile.Create(_facility, patient);
            noFacilityProfile.Facility = null;
            Assert.IsFalse(noFacilityProfile.IsValid());

            var noExtractsProfile = PatientLabProfile.Create(_facility, patient);
            noExtractsProfile.LaboratoryExtracts = null;
            Assert.IsFalse(noExtractsProfile.IsValid());

            noExtractsProfile = PatientLabProfile.Create(_facility, patient);
            noExtractsProfile.LaboratoryExtracts = new List<PatientLaboratoryExtractDTO>();
            Assert.IsFalse(noExtractsProfile.IsValid());
        }

        [Test]
        public void should_GeneratePatientRecord()
        {
            var patient = _patientExtracts.First();
            var artProfile = PatientLabProfile.Create(_facility, patient);

            artProfile.GeneratePatientRecord();

            Assert.IsNotNull(artProfile.FacilityInfo);
            Assert.IsNotNull(artProfile.PatientInfo);
            Assert.That(artProfile.LaboratoryExtracts.Count, Is.EqualTo(10));
            Assert.IsNull(artProfile.Extracts);
        }

        [Test]
        public void should_GenerateRecords()
        {
            var patient = _patientExtracts.First();
            var artProfile = PatientLabProfile.Create(_facility, patient);
            artProfile.GeneratePatientRecord();

            artProfile.GenerateRecords(patient.Id);

            Assert.That(artProfile.Extracts.Count, Is.EqualTo(10));
            Assert.AreEqual(artProfile.PatientInfo.Id, artProfile.Extracts.First().PatientId);
        }
    }
}