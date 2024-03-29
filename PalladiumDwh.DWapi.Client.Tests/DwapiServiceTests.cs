﻿using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.DWapi.Client.Tests
{
    [TestFixture]
    public class DwapiServiceTests
    {
        private readonly string url = "http://197.232.1.130:81/dwapi/api/";
        //private readonly string url ="http://localhost/dwapi/api/";
        private IDwapiService _dwapiService;
        private Facility _newFacility;
        private List<PatientExtract> _patientWithAllExtracts;
        private int patientCount = 5;
        private int extractCount = 5;

        [SetUp]
        public void SetUp()
        {
            _dwapiService = new DwapiService(url);
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
