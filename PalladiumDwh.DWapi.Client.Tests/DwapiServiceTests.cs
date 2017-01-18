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
            _patientWithAllExtracts = TestHelpers.GetTestPatientWithExtracts(_newFacility, 2, 2).ToList();
        }

        [Test]
        public void should_Get_Facility()
        {
            var facility = _dwapiService.Get(-1);
            Assert.IsNotNull(facility);
            Assert.AreEqual(-1, facility.Code);
        }
        [Test]
        public void should_Post()
        {
            var patient = _patientWithAllExtracts.First();

            var artProfile = PatientARTProfile.Create(_newFacility, patient);
            Console.WriteLine(JsonConvert.SerializeObject(artProfile));
            Console.WriteLine(new string('-',10));
            var baselineProfile = PatientBaselineProfile.Create(_newFacility, patient);
            Console.WriteLine(JsonConvert.SerializeObject(baselineProfile));
            Console.WriteLine(new string('-', 10));
            var labProfile = PatientLabProfile.Create(_newFacility, patient);
            Console.WriteLine(JsonConvert.SerializeObject(labProfile));
            Console.WriteLine(new string('-', 10));
            var pharmacyProfile = PatientPharmacyProfile.Create(_newFacility, patient);
            Console.WriteLine(JsonConvert.SerializeObject(pharmacyProfile));
            Console.WriteLine(new string('-', 10));
            var statusProfile = PatientStatusProfile.Create(_newFacility, patient);
            Console.WriteLine(JsonConvert.SerializeObject(statusProfile));
            Console.WriteLine(new string('-', 10));
            var visitProfile = PatientVisitProfile.Create(_newFacility, patient);
            Console.WriteLine(JsonConvert.SerializeObject(visitProfile));
            Console.WriteLine(new string('-', 10));

            Assert.IsTrue(_dwapiService.Post(artProfile));
            Assert.IsTrue(_dwapiService.Post(baselineProfile));
            Assert.IsTrue(_dwapiService.Post(labProfile));
            Assert.IsTrue(_dwapiService.Post(pharmacyProfile));
            Assert.IsTrue(_dwapiService.Post(statusProfile));
            Assert.IsTrue(_dwapiService.Post(visitProfile));
        }

    }
}
