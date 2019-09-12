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
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Infrastructure.Data.Repository;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.DWapi.Tests
{
    [TestFixture]
    public class SpotControllerTests
    {
        private readonly string _queueName = $@".\private$\dwapi.emr.concept";

        private static readonly string baseUrl = "http://localhost:88/api/Spot";

        private SpotController _controller;
        private List<PatientExtract> _patientWithAllExtracts;
        private Manifest _manifest;

        
        private DwapiCentralContext _dbcontext;
        private List<Facility> _facilities;
        private Facility _facilityA;
        private List<PatientExtract> _patients;
        private PatientExtractRepository _patientExtractRepository;
        private IMessagingSenderService _messagingService;
        private readonly ILiveSyncService _liveSyncService;
        [SetUp]
        public void SetUp()
        {
            _messagingService = new MessagingSenderService(_queueName);
            _dbcontext = new DwapiCentralContext();
            _dbcontext.Database.ExecuteSqlCommand($@"DELETE FROM [Facility] where Code=20612");


            _facilities = TestHelpers.GetTestData<Facility>(1).ToList();
            foreach (var f in _facilities)
            {
                f.Code = 20612;
            }
            TestHelpers.CreateTestData(_dbcontext, _facilities);
            _facilityA = _facilities.First();           
            _patients = TestHelpers.GetTestPatientData(_facilityA, 5).ToList();
            int pid = 1000;
            foreach (var p in _patients)
            {
                p.PatientPID = pid;
                pid++;
            }

            TestHelpers.CreateTestData(_dbcontext, _patients);
            

            _patientExtractRepository = new PatientExtractRepository(_dbcontext);


            

            _controller = new SpotController(_messagingService,_patientExtractRepository,_liveSyncService);
            TestHelpers.SetupControllerForTests(_controller, baseUrl, "Spot");

             _manifest = new Manifest(_facilityA.Code);
            var currentPatients = _patients.Where(x => x.PatientPID > 1000);
            foreach (var p in currentPatients)
            {
                _manifest.AddPatientPk(p.PatientPID);
            }
        }

        [Test]
        public void should_Post()
        {
            var result = _controller.Post(_manifest).Result;

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            var response = result.Content.ReadAsStringAsync().Result;
            Assert.IsTrue(!string.IsNullOrWhiteSpace(response));

            _patientExtractRepository = new PatientExtractRepository(new DwapiCentralContext());
            var cleanPatients = _patientExtractRepository.GetAllBy(x => x.FacilityId == _facilityA.Id).ToList();

            Assert.IsTrue(cleanPatients.Count == 4);

            Console.WriteLine($"{response}");
        }

        [Test]
        public void should_Not_Post_InvalidMFL()
        {
            _manifest.SiteCode = 111;
          
            var result = _controller.Post(_manifest).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            var response = result.Content.ReadAsStringAsync().Result;
            Assert.IsTrue(!string.IsNullOrWhiteSpace(response));

            Console.WriteLine($"{response}");
        }

        [TearDown]
        public void TearDown()
        {
            _dbcontext = new DwapiCentralContext();
            _dbcontext.Database.ExecuteSqlCommand($@"DELETE FROM [Facility] where Code={_facilityA.Code}");
        }
    }
}