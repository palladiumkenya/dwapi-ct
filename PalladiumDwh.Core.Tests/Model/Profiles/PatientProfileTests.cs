using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Tests.Model.Profiles
{
    [TestFixture]
    public class PatientProfileTests
    {
        private List<PatientExtract> _patientExtracts;
        private Facility _facility;

        [SetUp]
        public void SetUp()
        {
            _facility = Builder<Facility>.CreateNew().Build();
            _patientExtracts = TestHelpers.GetTestPatientData(_facility, 2).ToList();
        }

        [Test]
        public void should_Create_PatientProfile()
        {
            var patient = _patientExtracts.First();
            var artProfile = PatientProfile.Create(_facility, patient);

            Assert.IsNotNull(artProfile.Demographic);
            Assert.IsNotNull(artProfile.Facility);
            Assert.That(artProfile.ArtExtracts.Count, Is.EqualTo(0));
            Assert.That(artProfile.BaselinesExtracts.Count, Is.EqualTo(0));
            Assert.That(artProfile.LaboratoryExtracts.Count, Is.EqualTo(0));
            Assert.That(artProfile.PharmacyExtracts.Count, Is.EqualTo(0));
            Assert.That(artProfile.StatusExtracts.Count, Is.EqualTo(0));
            Assert.That(artProfile.VisitExtracts.Count, Is.EqualTo(0));
        }
    }
}