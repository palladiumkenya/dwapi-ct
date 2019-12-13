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
        private static readonly string syncUrl = "http://koske-mac:4777/stages/";
        private static readonly string metric ="{\"EmrName\":\"Demo EMR\",\"EmrVersion\":\"v1.0.0.0\",\"LastLoginDate\":\"1983-07-04T00:00:00\",\"LastMoH731RunDate\":\"1983-07-04T00:00:00\",\"DateExtracted\":\"2019-12-03T14:49:01.703992\",\"Id\":\"0f78f8fe-6396-4bf1-91da-ab1800c2bd90\"}";

        private SpotController _controller;
        private List<PatientExtract> _patientWithAllExtracts;
        private Manifest _manifest, _manifestNew;


        private DwapiCentralContext _dbcontext;
        private List<Facility> _facilities;
        private Facility _facilityA;
        private List<PatientExtract> _patients;
        private PatientExtractRepository _patientExtractRepository;
        private IFacilityRepository _facilityRepository;
        private IMessagingSenderService _messagingService;
        private  ILiveSyncService _liveSyncService;
        [SetUp]
        public void SetUp()
        {
            _messagingService = new MessagingSenderService(_queueName);
            _dbcontext = new DwapiCentralContext();
            _dbcontext.Database.ExecuteSqlCommand($@"DELETE FROM [Facility] where Code in (20612,10001)");



            _facilities = TestHelpers.GetTestData<Facility>(2).ToList();
            foreach (var f in _facilities)
            {
                f.Code = 20612;
            }

            _facilities[1].Code = 10001;

            TestHelpers.CreateTestData(_dbcontext, _facilities.Where(x => x.Code != 10001));
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
            _facilityRepository=new FacilityRepository(_dbcontext);
            _liveSyncService=new LiveSyncService(syncUrl);



            _controller = new SpotController(_messagingService,_patientExtractRepository,_liveSyncService,_facilityRepository);
            TestHelpers.SetupControllerForTests(_controller, baseUrl, "Spot");

             _manifest = new Manifest(_facilityA.Code);
             _manifest.Metrics = metric;
            var currentPatients = _patients.Where(x => x.PatientPID > 1000).ToList();
            foreach (var p in currentPatients)
            {
                _manifest.AddPatientPk(p.PatientPID);
            }
            _manifestNew=new Manifest(_facilities[1].Code);
            _manifestNew.Metrics = metric;
            foreach (var p in currentPatients)
            {
                _manifestNew.AddPatientPk(p.PatientPID);
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
        public void should_Post_And_Enroll()
        {
            var result = _controller.Post(_manifestNew).Result;

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            var response = result.Content.ReadAsStringAsync().Result;
            Assert.IsTrue(!string.IsNullOrWhiteSpace(response));

            var facilityRepository = new FacilityRepository(new DwapiCentralContext());
            var enrolledFacilty = facilityRepository.GetAllBy(x => x.Code == _manifestNew.SiteCode).FirstOrDefault();

            Assert.NotNull(enrolledFacilty);
            Assert.True(enrolledFacilty.ProfileMissing());

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
            _dbcontext.Database.ExecuteSqlCommand($@"DELETE FROM [Facility] where Code in ({_facilityA.Code},{_facilities[1].Code})");
        }
    }
}
