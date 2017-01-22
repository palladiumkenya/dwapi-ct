using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
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
        private int patientCount = 5;
        private int extractCount = 5;

        [SetUp]
        public void SetUp()
        {
            _dwapiService = new DwapiService("http://localhost/dwapi/api/");
            _newFacility = Builder<Facility>.CreateNew().Build();
            _patientWithAllExtracts = TestHelpers.GetTestPatientWithExtracts(_newFacility,patientCount, extractCount).ToList();

            /*
            new MessageQueue($@".\private$\dwapi.emr.{typeof(PatientARTProfile).Name.ToLower()}").Purge();
            new MessageQueue($@".\private$\dwapi.emr.{typeof(PatientBaselineProfile).Name.ToLower()}").Purge();
            new MessageQueue($@".\private$\dwapi.emr.{typeof(PatientLabProfile).Name.ToLower()}").Purge();
            new MessageQueue($@".\private$\dwapi.emr.{typeof(PatientPharmacyProfile).Name.ToLower()}").Purge();
            new MessageQueue($@".\private$\dwapi.emr.{typeof(PatientStatusProfile).Name.ToLower()}").Purge();
            new MessageQueue($@".\private$\dwapi.emr.{typeof(PatientVisitProfile).Name.ToLower()}").Purge();
            */

        }

        [Test]
        public void should_Post()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            

            foreach (var patient in _patientWithAllExtracts)
            {
                var artProfile = PatientARTProfile.Create(_newFacility, patient);
                var baselineProfile = PatientBaselineProfile.Create(_newFacility, patient);
                var labProfile = PatientLabProfile.Create(_newFacility, patient);
                var pharmacyProfile = PatientPharmacyProfile.Create(_newFacility, patient);
                var statusProfile = PatientStatusProfile.Create(_newFacility, patient);
                var visitProfile = PatientVisitProfile.Create(_newFacility, patient);

                Assert.IsTrue(_dwapiService.Post(artProfile));
                Assert.IsTrue(_dwapiService.Post(baselineProfile));
                Assert.IsTrue(_dwapiService.Post(labProfile));
                Assert.IsTrue(_dwapiService.Post(pharmacyProfile));
                Assert.IsTrue(_dwapiService.Post(statusProfile));
                Assert.IsTrue(_dwapiService.Post(visitProfile));
            }

            
            watch.Stop();
             var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"{patientCount} Patients with {extractCount} x 6 extracts each POST requested done in { elapsedMs} ms (  {elapsedMs/1000})");
        }

    }
}
