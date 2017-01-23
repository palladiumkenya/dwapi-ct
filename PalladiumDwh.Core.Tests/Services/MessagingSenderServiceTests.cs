using System;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Services;
using System.Collections.Generic;
using System.Messaging;
using PalladiumDwh.Shared;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Profiles;

namespace PalladiumDwh.Core.Tests.Services
{
    [TestFixture]
    public class MessagingSenderServiceTests
    {
        private readonly string _queueName = $@".\private$\dwapi.emr.concept";

        private IMessagingSenderService _messagingSenderService;
        private Facility _newFacility;
        private List<PatientExtract> _patientWithAllExtracts;

        [SetUp]
        public void SetUp()
        {
            //_queueName += DateTime.Now.Millisecond.ToString();
            _newFacility = Builder<Facility>.CreateNew().Build();
            _patientWithAllExtracts = TestHelpers.GetTestPatientWithExtracts(_newFacility, 2, 10).ToList();
            _messagingSenderService=new MessagingSenderService(_queueName);
        }

        [Test]
        public void should_Initialize()
        {
           _messagingSenderService.Initialize();
            var msmq = _messagingSenderService.Queue as MessageQueue;

            Assert.IsNotNull(msmq);
            Assert.AreEqual(msmq.Path,_queueName);
            Console.WriteLine(_messagingSenderService.QueueName);

            _messagingSenderService.Initialize("test");
            msmq = _messagingSenderService.Queue as MessageQueue;
            Assert.That(msmq.Path,Does.EndWith("test"));
            Console.WriteLine(_messagingSenderService.QueueName);
        }

        [Test]
        public void should_Send_To_Queue()
        {
            var gateway = typeof(PatientARTProfile).Name.ToLower();
            var patient = _patientWithAllExtracts.First();
            var profile = PatientARTProfile.Create(_newFacility, patient);
            profile.GeneratePatientRecord();
            profile.GenerateRecords(profile.PatientInfo.Id);

            _messagingSenderService.Initialize(gateway);
            var messageId = _messagingSenderService.Send(profile);

            var msmq = _messagingSenderService.Queue as MessageQueue;
            var message = msmq.GetAllMessages()
                .FirstOrDefault(x => x.Id.ToLower() == messageId.ToLower());

            Assert.IsNotNull(message);

            var prof = message.BodyStream.ReadFromJson<PatientARTProfile>();
            Console.WriteLine($"{prof}  Message Id [{messageId}]");
        }

        [TearDown]
        public void TearDown()
        {
            var msmq = _messagingSenderService.Queue as MessageQueue;
            msmq.Purge();
        }       
    }
}