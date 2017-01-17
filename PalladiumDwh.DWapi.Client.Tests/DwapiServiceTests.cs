using System;
using System.Linq;
using NUnit.Framework.Internal;
using NUnit.Framework;
using PalladiumDwh.DWapi.Client.Model.Profiles;

namespace PalladiumDwh.DWapi.Client.Tests
{
    [TestFixture]
    public class DwapiServiceTests
    {
        private IDwapiService _dwapiService;

        [SetUp]
        public void SetUp()
        {
            _dwapiService = new DwapiService("http://localhost/dwapi/api/");
        }

        [Test]
        public void should_Get_Facility()
        {
            //PatientArt / 13784Code 15238   Neal Mississippi Facility

                        var facility = _dwapiService.Get(15238);

            Assert.IsNotNull(facility);
            Assert.AreEqual(15238, facility.Code);
        }
        [Test]
        public void should_Post()
        {
            var facility = _dwapiService.Get(15238);
            Assert.IsNotNull(facility);
            var extarcts=TestHelpers.GetTestPatientARTData(facility, 10, 100);

            var profile = extarcts.First();

            var artProfile = PatientARTProfile.Create(facility, profile);
            var resposne = _dwapiService.Post(artProfile);
            
            Assert.That(resposne,Is.Not.Null.Or.EqualTo(Guid.Empty));
        }
    }
}
