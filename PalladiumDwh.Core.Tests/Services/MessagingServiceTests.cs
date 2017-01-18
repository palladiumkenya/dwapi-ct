using System;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Core.Model.Profiles;
using PalladiumDwh.Core.Services;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Infrastructure.Data.Repository;
using System.Collections.Generic;
using System.Messaging;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Tests.Services
{
    [TestFixture]
    public class MessagingServiceTests
    {
        private readonly string _queueName = $@".\private$\dw.emrpatient.concept";

        private IMessagingService _messagingService;
        private Facility _newFacility;
        private List<PatientExtract> _patientWithAllExtracts;

        [SetUp]
        public void SetUp()
        {
            //_queueName += DateTime.Now.Millisecond.ToString();
            _newFacility = Builder<Facility>.CreateNew().Build();
            _patientWithAllExtracts = TestHelpers.GetTestPatientWithExtracts(_newFacility, 2, 10).ToList();
            _messagingService=new MessagingService(_queueName);
        }

        [Test]
        public void should_Initialize()
        {
           _messagingService.Initialize();
            var msmq = _messagingService.Queue as MessageQueue;

            Assert.IsNotNull(msmq);
            Assert.AreEqual(msmq.Path,_queueName);
            Console.WriteLine(_messagingService.QueueName);
        }

        [Test]
        public void should_Send_To_Queue()
        {
            var patient = _patientWithAllExtracts.First();
            var profile = PatientARTProfile.Create(_newFacility, patient);
            profile.GeneratePatientRecord();
            profile.GenerateRecords(profile.PatientInfo.Id);

            _messagingService.Initialize();
            var messageId = _messagingService.Send(profile);

            var msmq = _messagingService.Queue as MessageQueue;
            var message = msmq.GetAllMessages()
                .FirstOrDefault(x => x.Id.ToLower() == messageId.ToLower());

            Assert.IsNotNull(message);

            var prof = message.BodyStream.ReadFromJson<PatientARTProfile>();
            Console.WriteLine($"{prof}  Message Id [{messageId}]");
        }

        [TearDown]
        public void TearDown()
        {
            var msmq = _messagingService.Queue as MessageQueue;
            msmq.Purge();
        }
        
    }
}