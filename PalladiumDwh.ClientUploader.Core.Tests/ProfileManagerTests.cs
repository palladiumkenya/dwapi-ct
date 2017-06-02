using System;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Profile;
using PalladiumDwh.ClientUploader.Core.Interfaces;

namespace PalladiumDwh.ClientUploader.Core.Tests
{
    [TestFixture]
    public class ProfileManagerTests
    {
        private ClientPatientExtract _patientExtract;
        private IProfileManager _profileManager;

        [SetUp]
        public void SetUp()
        {
            _patientExtract = TestHelpers.GetTestPatientWithExtracts(1, 5).First();
            _profileManager=new ProfileManager();
        }

        [Test]
        public void should_Decord_PatientProfile()
        {
            var exracts = _profileManager.Generate(_patientExtract).ToList();
            Assert.IsTrue(exracts.Count > 0);
        }

        [Test]
        public void should_Generate_Profiles()
        {
            var exracts = _profileManager.Generate(_patientExtract).ToList();
            Assert.IsTrue(exracts.Count>0);
        }

        [Test]
        public void should_Generate_Site_Profiles()
        {
            var exracts = _profileManager.Generate(_patientExtract).ToList();
            Assert.IsTrue(exracts.Count > 0);
        }
        [TestCase(typeof(ClientPatientARTProfile), 1,5)]
        [TestCase(typeof(ClientPatientBaselineProfile), 1, 5)]
        [TestCase(typeof(ClientPatientLabProfile), 1, 5)]
        [TestCase(typeof(ClientPatientPharmacyProfile), 1, 5)]
        [TestCase(typeof(ClientPatientStatusProfile), 1, 5)]
        [TestCase(typeof(ClientPatientVisitProfile), 1, 5)]
        public void should_Generate_Profiles_AllTypes(Type profile,int count,int extractCount)
        {
            var exracts = _profileManager.Generate(_patientExtract).ToList();

            var profiles = exracts.Where(x => x.GetType() == profile).ToList();
            Assert.IsTrue(profiles.Count == count);
            var extractProfile = profiles.First();
            if (extractProfile is ClientPatientARTProfile)
            {
                var eCount = ((ClientPatientARTProfile) extractProfile).ArtExtracts.Count;
                Assert.IsTrue(eCount == extractCount);
                Console.WriteLine($"Has {count} {profile.Name} with {eCount} Extracts");
            }
            if (extractProfile is ClientPatientBaselineProfile)
            {
                var eCount = ((ClientPatientBaselineProfile)extractProfile).BaselinesExtracts.Count;
                Assert.IsTrue(eCount == extractCount);
                Console.WriteLine($"Has {count} {profile.Name} with {eCount} Extracts");
            }
            if (extractProfile is ClientPatientLabProfile)
            {
                var eCount = ((ClientPatientLabProfile)extractProfile).LaboratoryExtracts.Count;
                Assert.IsTrue(eCount == extractCount);
                Console.WriteLine($"Has {count} {profile.Name} with {eCount} Extracts");
            }
            if (extractProfile is ClientPatientPharmacyProfile)
            {
                var eCount = ((ClientPatientPharmacyProfile)extractProfile).PharmacyExtracts.Count;
                Assert.IsTrue(eCount == extractCount);
                Console.WriteLine($"Has {count} {profile.Name} with {eCount} Extracts");
            }
            if (extractProfile is ClientPatientStatusProfile)
            {
                var eCount = ((ClientPatientStatusProfile)extractProfile).StatusExtracts.Count;
                Assert.IsTrue(eCount == extractCount);
                Console.WriteLine($"Has {count} {profile.Name} with {eCount} Extracts");
            }
            if (extractProfile is ClientPatientVisitProfile)
            {
                var eCount = ((ClientPatientVisitProfile)extractProfile).VisitExtracts.Count;
                Assert.IsTrue(eCount == extractCount);
                Console.WriteLine($"Has {count} {profile.Name} with {eCount} Extracts");
            }
        }
    }
}
