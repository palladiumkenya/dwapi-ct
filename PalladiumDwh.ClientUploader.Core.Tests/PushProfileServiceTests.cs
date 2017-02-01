using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Profile;
using PalladiumDwh.ClientUploader.Core.Interfaces;
using PalladiumDwh.ClientUploader.Core.Services;

namespace PalladiumDwh.ClientUploader.Core.Tests
{
    [TestFixture]
    public class PushProfileServiceTests
    {
        private IPushProfileService _service;
        private string _url = "http://localhost/dwapi/api/";
        private ClientPatientExtract _patientExtract;
        private ProfileManager _profileManager;
        private IEnumerable<IClientExtractProfile> _profiles;

        [SetUp]
        public void SetUp()
        {
            _service=new PushProfileService(_url);
            _patientExtract = TestHelpers.GetTestPatientWithExtracts(1, 5).First();
            _profileManager = new ProfileManager();
            _profiles=_profileManager.Generate(_patientExtract);
        }

        [Test]
        public void Should_Push()
        {
            var extractProfile = _profiles.OfType<ClientPatientARTProfile>().First();
            Assert.IsNotNull(extractProfile);

            var status = _service.PushAsync(extractProfile).Result;
            Assert.IsTrue(status.IsSuccessStatusCode);
        }
    }
}