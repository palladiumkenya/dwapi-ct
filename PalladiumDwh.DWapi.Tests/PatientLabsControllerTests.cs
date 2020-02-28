using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Net;
using FizzWare.NBuilder;
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
    public class PatientLabsControllerTests
    {
        private readonly string _queueName = $@".\private$\dwapi.emr.concept";

        private static readonly string baseUrl = "http://localhost/api/PatientArt";

        private PatientLabsController _controller;
        private List<PatientExtract> _patientWithAllExtracts;
        private Facility _facility;

        private IMessagingSenderService _messagingService;

        [SetUp]
        public void SetUp()
        {
            _messagingService = new MessagingSenderService(_queueName);

            _controller = new PatientLabsController(_messagingService,new MessengerScheduler());
            TestHelpers.SetupControllerForTests(_controller, baseUrl, "PatientLabs");

            _facility = Builder<Facility>.CreateNew().Build();
            _patientWithAllExtracts = TestHelpers.GetTestPatientWithExtracts(_facility, 2, 10).ToList();
        }

        [Test]
        public void should_Post()
        {
            var patient = _patientWithAllExtracts.First();
            var profile = PatientLabProfile.Create(_facility, patient);

            var result = _controller.Post(profile).Result;

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            var messageId = result.Content.ReadAsStringAsync().Result;
            Assert.IsTrue(!string.IsNullOrWhiteSpace(messageId));

            Console.WriteLine($"Message Id [{messageId}]");
        }

        [Test]
        public void should_PostBatch()
        {
            var profile = PatientLabProfile.Create(_facility, _patientWithAllExtracts);

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
