using System;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Services;
using System.Collections.Generic;
using System.Messaging;
using PalladiumDwh.Shared.Extentions;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Tests.Services
{
    [TestFixture]
    public class MessagingSenderServiceTests
    {
        private readonly string _queueName = $@".\private$\dwapi.emr.concept";

        private IMessagingSenderService _messagingSenderService;
        private Facility _newFacility;
        private List<PatientExtract> _patientWithAllExtracts;
        private List<string> _allGateways;

        [SetUp]
        public void SetUp()
        {
            //_queueName += DateTime.Now.Millisecond.ToString();
            _newFacility = Builder<Facility>.CreateNew().Build();
            _patientWithAllExtracts = TestHelpers.GetTestPatientWithExtracts(_newFacility, 2, 10).ToList();
            _messagingSenderService = new MessagingSenderService(_queueName);

        }

        [Test]
        public void should_Initialize()
        {
            _messagingSenderService.Initialize();
            var msmq = _messagingSenderService.Queue as MessageQueue;

            Assert.IsNotNull(msmq);
            Assert.AreEqual(msmq.Path, _queueName);
            Console.WriteLine(_messagingSenderService.QueueName);

            _messagingSenderService.Initialize("test");
            msmq = _messagingSenderService.Queue as MessageQueue;
            Assert.That(msmq.Path, Does.EndWith("test"));
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

        [TestCase(@".\private$\dwapi.emr.")]
        [TestCase(@".\private$\dwapi.emr.concept.")]
        public void should_GetMessageCount(string name)
        {
            
            _allGateways = TestHelpers.GetGateways(name);
            foreach (var gateway in _allGateways)
            {
                _messagingSenderService = new MessagingSenderService(gateway);_messagingSenderService.Initialize();
                var count = _messagingSenderService.GetNumberOfMessages(gateway);
                Assert.IsTrue(count > -1);
                Console.WriteLine($"{gateway}:{count}");
            }
        }

        [TestCase(@".\private$\dwapi.emr.")]
        [TestCase(@".\private$\dwapi.emr.concept.")]
        public void should_GetJounralMessageCount(string name)
        {
            _allGateways = TestHelpers.GetGateways(name);
            foreach (var gateway in _allGateways)
            {
                _messagingSenderService = new MessagingSenderService(gateway); _messagingSenderService.Initialize();
                var count = _messagingSenderService.GetNumberOfMessages(gateway,true);
                Assert.IsTrue(count > -1);
                Console.WriteLine($@"{gateway}\Journal$:{count}");
            }
        }
        [TestCase(@".\private$\dwapi.emr.")]
        [TestCase(@".\private$\dwapi.emr.concept.")]
        public void should_Purge_Queues(string name)
        {
            _allGateways = TestHelpers.GetGateways(name);
            foreach (var gateway in _allGateways)
            {
                _messagingSenderService = new MessagingSenderService(gateway); _messagingSenderService.Initialize();
                _messagingSenderService.Purge(gateway);

                var count = _messagingSenderService.GetNumberOfMessages(gateway);
                Assert.IsTrue(count == 0);
                Console.WriteLine($@"{gateway}:{count}");
            }
        }

        [TestCase(@".\private$\dwapi.emr.")]
        [TestCase(@".\private$\dwapi.emr.concept.")]
        public void should_Purge_JournalQueues(string name)
        {
            _allGateways = TestHelpers.GetGateways(name);
            foreach (var gateway in _allGateways)
            {
                _messagingSenderService = new MessagingSenderService(gateway); _messagingSenderService.Initialize();
                _messagingSenderService.Purge(gateway,true);

                var count = _messagingSenderService.GetNumberOfMessages(gateway, true);
                Assert.IsTrue(count == 0);
                Console.WriteLine($@"{gateway}\Journal$:{count}");
            }
        }



        [TearDown]
        public void TearDown()
        {
            try
            {
                var msmq = _messagingSenderService.Queue as MessageQueue;
                msmq.Purge();
            }
            catch 
            {
                
                
            }
            
        }       
    }
}