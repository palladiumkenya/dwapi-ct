using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using Newtonsoft.Json;
using NUnit.Framework.Internal;
using NUnit.Framework;
using PalladiumDwh.DWapi.Client.Model;
using PalladiumDwh.DWapi.Client.Model.Profiles;

namespace PalladiumDwh.DWapi.Client.Tests
{
    [TestFixture]
    public class DwapiServiceTests
    {
        private IDwapiService _dwapiService;
        private Facility _newFacility;
        private List<PatientExtract> _patientWithAllExtracts;

        [SetUp]
        public void SetUp()
        {
            _dwapiService = new DwapiService("http://localhost/dwapi/api/");
            _newFacility = Builder<Facility>.CreateNew().Build();
            _patientWithAllExtracts = TestHelpers.GetTestPatientWithExtracts(_newFacility, 2, 10).ToList();
        }

        [Test]
        public void should_Get_Facility()
        {
            //PatientArt / 13784Code 15238   Neal Mississippi Facility

             var facility = _dwapiService.Get(-1);

            Assert.IsNotNull(facility);
            Assert.AreEqual(-1, facility.Code);
        }
        [Test]
        public void should_Post()
        {
            var patient = _patientWithAllExtracts.First();

            //Assert.IsTrue(_dwapiService.Post(PatientARTProfile.Create(_newFacility, patient)));
            Assert.IsTrue(_dwapiService.Post(PatientBaselineProfile.Create(_newFacility, patient)));
            Assert.IsTrue(_dwapiService.Post(PatientLabProfile.Create(_newFacility, patient)));
            Assert.IsTrue(_dwapiService.Post(PatientPharmacyProfile.Create(_newFacility, patient)));
            Assert.IsTrue(_dwapiService.Post(PatientStatusProfile.Create(_newFacility, patient)));
            Assert.IsTrue(_dwapiService.Post(PatientVisitProfile.Create(_newFacility, patient)));
            
        }
    }
}
