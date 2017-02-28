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
        //dwapi.emr.concept
        private readonly string _queueName = $@".\private$\dwapi.emr";

        private IMessagingSenderService _messagingSenderService;
        private Facility _newFacility;
        private List<PatientExtract> _patientWithAllExtracts;
        private List<string> _allGateways;

        [SetUp]
        public void SetUp()
        {
            _newFacility = Builder<Facility>.CreateNew().Build();
            _patientWithAllExtracts = TestHelpers.GetTestPatientWithExtracts(_newFacility, 2, 10).ToList();
            _messagingSenderService = new MessagingSenderService(_queueName);
        }

        [Test,Order(1)]
        public void should_Initialize()
        {
            _messagingSenderService.Initialize();

            var msmq = _messagingSenderService.Queue as MessageQueue;
            var msmqB = _messagingSenderService.BacklogQueue as MessageQueue;

            Assert.IsNotNull(msmq);
            Assert.IsNotNull(msmqB);

            Assert.AreEqual(msmq.Path, _queueName);
            Assert.AreEqual(msmqB.Path, $"{_queueName}.backlog");
            
            Console.WriteLine(_messagingSenderService.QueueName);
            Console.WriteLine(_messagingSenderService.BacklogQueueName);

            MessageQueue.Delete(msmq.Path);
            MessageQueue.Delete(msmqB.Path);
        }

        [Test,Order(2)]
        public void should_Initialize_With_Gateway()
        {
            _messagingSenderService.Initialize("test");

            var msmq = _messagingSenderService.Queue as MessageQueue;
            var msmqB = _messagingSenderService.BacklogQueue as MessageQueue;

            Assert.IsNotNull(msmq);
            Assert.IsNotNull(msmqB);
      
            Assert.That(msmq.Path, Does.EndWith("test"));
            Assert.That(msmqB.Path, Does.EndWith("test.backlog"));

            Console.WriteLine(_messagingSenderService.QueueName);
            Console.WriteLine(_messagingSenderService.BacklogQueueName);

            MessageQueue.Delete(msmq.Path);
            MessageQueue.Delete(msmqB.Path);
        }

        [Test, Order(3)]
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

        [Test, Order(3)]
        public void should_Send_Async_To_Queue()
        {
            var gateway = typeof(PatientARTProfile).Name.ToLower();
            var patient = _patientWithAllExtracts.First();
            var profile = PatientARTProfile.Create(_newFacility, patient);
            profile.GeneratePatientRecord();
            profile.GenerateRecords(profile.PatientInfo.Id);

            _messagingSenderService.Initialize(gateway);
            var messageId = _messagingSenderService.SendAsync(profile).Result;

            var msmq = _messagingSenderService.Queue as MessageQueue;
            var message = msmq.GetAllMessages()
                .FirstOrDefault(x => x.Id.ToLower() == messageId.ToLower());

            Assert.IsNotNull(message);

            var prof = message.BodyStream.ReadFromJson<PatientARTProfile>();
            Console.WriteLine($"{prof}  Message Id [{messageId}]");
        }

        [Order(5)]
        [TestCase(@".\private$\dwapi.emr.")]
        //[TestCase(@".\private$\dwapi.emr.concept.")]
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
        [Order(6)]
        [TestCase(@".\private$\dwapi.emr.")]
        //[TestCase(@".\private$\dwapi.emr.concept.")]
        public void should_GetBacklogMessageCount(string name)
        {

            _allGateways = TestHelpers.GetGateways(name);
            foreach (var gateway in _allGateways)
            {
                
                _messagingSenderService = new MessagingSenderService($"{gateway}"); _messagingSenderService.Initialize();
                var count = _messagingSenderService.GetNumberOfMessages($"{gateway}.backlog");
                Assert.IsTrue(count > -1);
                Console.WriteLine($"{gateway}.backlog:{count}");
            }
        }
        [Order(7)]
        [TestCase(@".\private$\dwapi.emr.")]
        //[TestCase(@".\private$\dwapi.emr.concept.")]
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
        [Order(8)]
        [TestCase(@".\private$\dwapi.emr.")]
        //[TestCase(@".\private$\dwapi.emr.concept.")]
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
        [Order(9)]
        [TestCase(@".\private$\dwapi.emr.")]
        //[TestCase(@".\private$\dwapi.emr.concept.")]
        public void should_Purge_BaclkogQueues(string name)
        {
            _allGateways = TestHelpers.GetGateways(name);
            foreach (var gateway in _allGateways)
            {
                _messagingSenderService = new MessagingSenderService($"{gateway}"); _messagingSenderService.Initialize();
                _messagingSenderService.Purge($"{gateway}.backlog");

                var count = _messagingSenderService.GetNumberOfMessages($"{gateway}.backlog");
                Assert.IsTrue(count == 0);
                Console.WriteLine($"{gateway}:{ count}");
            }
        }
        [Order(10)]
        [TestCase(@".\private$\dwapi.emr.")]
        //[TestCase(@".\private$\dwapi.emr.concept.")]
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

        [Order(11)]
        [TestCase(@".\private$\dwapi.emr.")]
        //[TestCase(@".\private$\dwapi.emr.concept.")]
        public void should_Delete_All_Queues(string name)
        {
            _allGateways = TestHelpers.GetAllGateways(name);
            foreach (var gateway in _allGateways)
            {
                _messagingSenderService = new MessagingSenderService(gateway);
                _messagingSenderService.Delete(gateway);
            }
            Assert.IsTrue(true);
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

        public void DeleteQueues(string name)
        {
            _allGateways = TestHelpers.GetAllGateways(name);
            foreach (var gateway in _allGateways)
            {
                _messagingSenderService = new MessagingSenderService(gateway);
                _messagingSenderService.Delete(gateway);
            }
        }
    }
}