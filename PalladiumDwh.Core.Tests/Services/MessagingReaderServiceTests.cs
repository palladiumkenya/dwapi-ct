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
    public class MessagingReaderServiceTests
    {
        private readonly string _queueName = $@".\private$\dw.emrpatient.concept";

        private IMessagingReaderService _messagingService;
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
                _patientVisitRepository = new PatientVisitRepository(_context)
            );

            _newFacility = _facilities.Last();
            _patientWithAllExtracts = TestHelpers.GetTestPatientWithExtracts(_newFacility, 2, 10).ToList();

            _messagingService = new MessagingReaderService(_syncService, _queueName);

            var patient = _patientWithAllExtracts.First();
            _profile = PatientARTProfile.Create(_newFacility, patient);
            _profile.GeneratePatientRecord();

            var sendService = new MessagingService(_queueName);
            sendService.Send(_profile);
        }

        [Test]
        public void should_Initialize()
        {
           _messagingService.Initialize();
            var msmq = _messagingService.Queue as MessageQueue;

            Assert.IsNotNull(msmq);
            Assert.AreEqual(msmq.Path,_queueName);
            Assert.IsTrue(msmq.Count() > 0);
            Console.WriteLine(_messagingService.QueueName);
        }

        [Test]
        public void should_Read_Queue()
        {
            _messagingService.Initialize();
            _messagingService.Read();

            var savedPatient = _patientExtractRepository.Find(x=>x.PatientCccNumber==_profile.PatientInfo.PatientCccNumber);
            Assert.IsNotNull(savedPatient);
            Assert.IsTrue(savedPatient.PatientArtExtracts.Count > 0);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
            var msmq = _messagingService.Queue as MessageQueue;
            msmq.Purge();
        }
        
    }
}