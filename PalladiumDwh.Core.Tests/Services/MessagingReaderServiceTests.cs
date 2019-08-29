using System;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Services;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Infrastructure.Data.Repository;
using System.Collections.Generic;
using System.Messaging;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Extentions;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Tests.Services
{
    [TestFixture]
    public class MessagingReaderServiceTests
    {
        private readonly string _queueName = $@".\private$\dwapi.emr.concept";

        private IMessagingReaderService _messagingReaderService;
        private Facility _newFacility;
        private List<PatientExtract> _patientWithAllExtracts;

        private DwapiCentralContext _context;
        private IFacilityRepository _facilityRepository;
        private IPatientExtractRepository _patientExtractRepository;
        private IPatientArtExtractRepository _patientArtExtractRepository;
        private IPatientBaseLinesRepository _patientBaseLinesRepository;
        private IPatientLabRepository _patientLabRepository;
        private IPatientPharmacyRepository _patientPharmacyRepository;

        private IPatientStatusRepository _patientStatusRepository;
        private IPatientVisitRepository _patientVisitRepository;
        private IPatientAdverseEventRepository _patientAdverseEventRepository;
        private SyncService _syncService;
        private PatientARTProfile _profile;
        private List<Facility> _facilities;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiCentralContext(Effort.DbConnectionFactory.CreateTransient(), true);
            _facilities = TestHelpers.GetTestData<Facility>(5).ToList();
            TestHelpers.CreateTestData(_context, _facilities);

            _syncService = new SyncService(
                _facilityRepository = new FacilityRepository(_context),
                _patientExtractRepository = new PatientExtractRepository(_context),
                _patientArtExtractRepository = new PatientArtExtractRepository(_context),
                _patientBaseLinesRepository = new PatientBaseLinesRepository(_context),
                _patientLabRepository = new PatientLabRepository(_context),
                _patientPharmacyRepository = new PatientPharmacyRepository(_context),
                _patientStatusRepository = new PatientStatusRepository(_context),
                _patientVisitRepository = new PatientVisitRepository(_context),
                _patientAdverseEventRepository=new PatientAdverseEventRepository(_context),
                null
            );

            _newFacility = _facilities.Last();
            _patientWithAllExtracts = TestHelpers.GetTestPatientWithExtracts(_newFacility, 2, 10).ToList();

            _messagingReaderService = new MessagingReaderService(_syncService, _queueName);

            var patient = _patientWithAllExtracts.First();
            _profile = PatientARTProfile.Create(_newFacility, patient);
            _profile.GeneratePatientRecord();

            var sendService = new MessagingSenderService(_queueName);
            sendService.Send(_profile);
        }

        [Test]
        public void should_Initialize()
        {
           _messagingReaderService.Initialize();
            var msmq = _messagingReaderService.Queue as MessageQueue;

            Assert.IsNotNull(msmq);
            Assert.AreEqual(msmq.Path,_queueName);
            Assert.IsTrue(msmq.Count() > 0);
            Console.WriteLine(_messagingReaderService.QueueName);
        }

        [Test]
        public void should_Read_Queue()
        {
            _messagingReaderService.Initialize();
            _messagingReaderService.Read();

            var savedPatient = _patientExtractRepository.Find(x=>x.PatientCccNumber==_profile.PatientInfo.PatientCccNumber);
            Assert.IsNotNull(savedPatient);
            Assert.IsTrue(savedPatient.PatientArtExtracts.Count > 0);
        }
        [Test]
        public void should_Move_To_Backlog_Queue()
        {
            var message=Utility.CreateMessage(_profile);

            _messagingReaderService.Initialize();
            _messagingReaderService.MoveToBacklog(message);

            var msmq = new MessageQueue(_messagingReaderService.BacklogQueueName);
            Assert.IsNotNull(msmq);
            Assert.IsTrue(msmq.Count() > 0);
            Console.WriteLine($"{_messagingReaderService.BacklogQueueName}:{msmq.Count()}");
        }
        [Test]
        public void should_Move_To_Backlog_Queue_On_Error()
        {
            var patient = _patientWithAllExtracts.Last();
            _profile = PatientARTProfile.Create(_newFacility, patient);
            _profile.GeneratePatientRecord();
            foreach (var e in _profile.ArtExtracts)
            {
                e.LastRegimen = new string('A', 151);
            }
            var sendService = new MessagingSenderService(_queueName);
            sendService.Send(_profile);

            _messagingReaderService.Initialize();
            _messagingReaderService.Read();

            var msmq = new MessageQueue(_messagingReaderService.BacklogQueueName);
            Assert.IsNotNull(msmq);
            Assert.IsTrue(msmq.Count() > 0);
            Console.WriteLine(_messagingReaderService.BacklogQueueName);
        }


        [Test]
        public void should_Proccess_Backlog_Queue()
        {
            var message = Utility.CreateMessage(_profile);
            _messagingReaderService.Initialize();
            _messagingReaderService.MoveToBacklog(message);
            var msmq = new MessageQueue(_messagingReaderService.BacklogQueueName);
            Assert.IsNotNull(msmq);
            Assert.IsTrue(msmq.Count() > 0);
            Console.WriteLine("before");
            Console.WriteLine($"{_messagingReaderService.BacklogQueueName}:{msmq.Count()}");
            Console.WriteLine(new string('-', 40));

            _messagingReaderService.Initialize();
            _messagingReaderService.PrcocessBacklog();
            Assert.IsTrue(msmq.Count()== 0);
            Console.WriteLine($"{_messagingReaderService.BacklogQueueName}:{msmq.Count()}");
        }


        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
            var msmq = _messagingReaderService.Queue as MessageQueue;
            msmq.Purge();
            var msmqBacklog = _messagingReaderService.BacklogQueue as MessageQueue;
            msmqBacklog.Purge();
        }
        
    }
}