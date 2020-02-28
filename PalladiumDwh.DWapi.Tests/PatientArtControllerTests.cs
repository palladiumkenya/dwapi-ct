using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Net;
using FizzWare.NBuilder;
using Newtonsoft.Json;
using NUnit.Framework;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Services;
using PalladiumDwh.DWapi.Controllers;
using PalladiumDwh.DWapi.Helpers;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.DWapi.Tests
{
    [TestFixture]
    public class PatientArtControllerTests
    {
        private readonly string _queueName = $@".\private$\dwapi.emr.concept";

        private static readonly string baseUrl = "http://localhost/api/PatientArt";

        private PatientArtController _controller;
        private List<PatientExtract> _patientWithAllExtracts;
        private Facility _facility;

        private IMessagingSenderService _messagingService;
        private IMessengerScheduler _messengerScheduler;

        [SetUp]
        public void SetUp()
        {
            _messagingService=new MessagingSenderService(_queueName);
            _messengerScheduler = new MessengerScheduler();
            _messengerScheduler.Start();

            _controller = new PatientArtController(_messagingService, _messengerScheduler);
            
            TestHelpers.SetupControllerForTests(_controller, baseUrl, "PatientArt");

            _facility = Builder<Facility>.CreateNew().Build();
            _patientWithAllExtracts = TestHelpers.GetTestPatientWithExtracts(_facility, 2, 10).ToList();
        }

        [Test]
        public void should_Post()
        {
            var patient = _patientWithAllExtracts.First();
            var profile = PatientARTProfile.Create(_facility, patient);

            var result = _controller.Post(profile).Result;

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            var messageId = result.Content.ReadAsStringAsync().Result;
            Assert.IsTrue(!string.IsNullOrWhiteSpace(messageId));

            Console.WriteLine($"Message Id [{messageId}]");
        }

        [Test]
        public void should_PostBatch()
        {
            var profile = PatientARTProfile.Create(_facility, _patientWithAllExtracts);
            var json = JsonConvert.SerializeObject(profile);
            var result = _controller.PostBatch(profile).Result;

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            var messageId = result.Content.ReadAsStringAsync().Result;
            Assert.IsTrue(!string.IsNullOrWhiteSpace(messageId));

            Console.WriteLine($"Message Ids [{messageId}]");
        }

        [TearDown]
        public void TearDown()
        {
            var msmq = _messagingService.Queue as MessageQueue;
            msmq.Purge();
        }
    }
}
