using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Core.Model.Profiles;
using PalladiumDwh.Infrastructure.Data;

namespace PalladiumDwh.Core.Tests.Model
{
    [TestFixture]
    public class PatientARTProfileTests
    {
        private List<PatientExtract> _patientExtracts;
        private PatientArtExtract _patientArtExtract;
        private Facility _facility;
        private PatientARTProfile _patientArtProfile;
        [SetUp]
        public void SetUp()
        {
            _facility= Builder<Facility>.CreateNew().Build();
            _patientExtracts = TestHelpers.GetTestPatientArtData(_facility, 2, 10).ToList();
            
        }
        [Test]
        public void should_GeneratePatientRecord()
        {
            var patient = _patientExtracts.First();
            var artProfile = PatientARTProfile.Create(_facility, patient);

            artProfile.GeneratePatientRecord();
            Assert.IsNotNull(artProfile.FacilityInfo);
            Assert.IsNotNull(artProfile.PatientInfo);
            Assert.That(artProfile.PatientArtExtracts.Count, Is.EqualTo(0));

        }
        [Test]
        public void should_GenerateRecords()
        {
            var patient = _patientExtracts.First();
            var artProfile = PatientARTProfile.Create(_facility, patient);
            artProfile.GeneratePatientRecord();
            artProfile.GenerateRecords();

            Assert.IsNotNull(artProfile.FacilityInfo);
            Assert.IsNotNull(artProfile.PatientInfo);
            Assert.That(artProfile.PatientArtExtracts.Count, Is.EqualTo(10));
        }
        [Test]
        public void should_Create_PatientARTProfile()
        {
            var patient = _patientExtracts.First();
            var artProfile = PatientARTProfile.Create(_facility, patient);
            
            Assert.IsNotNull(artProfile.Demographic);
            Assert.IsNotNull(artProfile.Facility);
            Assert.That(artProfile.ArtExtracts.Count,Is.EqualTo(10));
        }
    }
}
