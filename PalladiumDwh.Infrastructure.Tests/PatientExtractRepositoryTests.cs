using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Infrastructure.Data.Repository;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;


namespace PalladiumDwh.Infrastructure.Tests
{
    [TestFixture]
    public class PatientExtractRepositoryTests 
    {
        private DwapiCentralContext _context;
        private DwapiCentralContext _dbcontext;

        private List<Facility> _facilities;
        private IPatientExtractRepository _patientExtractRepository;
        private Facility _facilityA;
        private List<PatientExtract> _patients;

        [SetUp]
        public void SetUp()
        {
            _dbcontext = new DwapiCentralContext();
            

            _context = new DwapiCentralContext(Effort.DbConnectionFactory.CreateTransient(), true);
            _facilities = TestHelpers.GetTestData<Facility>(5).ToList();
            TestHelpers.CreateTestData(_context, _facilities);
            _facilityA = _facilities.First();
            _facilityA.Code = 999999;
            _dbcontext.Database.ExecuteSqlCommand($@"DELETE FROM [Facility] where Code={_facilityA.Code}");
            TestHelpers.CreateTestData(_dbcontext, new List<Facility> {_facilityA});
            _patients = TestHelpers.GetTestPatientData(_facilityA, 5, 10).ToList();
            int pid = 1000;
            foreach (var p in _patients)
            {
                p.PatientPID = pid;
                pid++;
            }

            TestHelpers.CreateTestData(_dbcontext, _patients);
            TestHelpers.CreateTestData(_context, _patients);

            _patientExtractRepository = new PatientExtractRepository(_context);
        }

      
        [Test]
        public void should_Sync_New()
        {
            var patientExtract = Builder<PatientExtract>.CreateNew().Build();
            patientExtract.PatientPID = DateTime.UtcNow.Millisecond;
            patientExtract.FacilityId = _facilityA.Id;
            Console.WriteLine(patientExtract.PatientPID);

            _patientExtractRepository.Sync(patientExtract);

            var saved = _patientExtractRepository.Find(patientExtract.Id);
            Assert.IsNotNull(saved);
            Assert.AreEqual(saved.PatientPID, patientExtract.PatientPID);
            Console.WriteLine(saved.PatientPID);
        }

        [Test]
        public void should_Sync_Exisitng()
        {
            var patientExtract = _patients.Last();
            Console.WriteLine(patientExtract.PatientPID);

            var newPatientExtract = Builder<PatientExtract>.CreateNew().Build();
            newPatientExtract.PatientPID = patientExtract.PatientPID;
            newPatientExtract.FacilityId = patientExtract.FacilityId;
            Console.WriteLine(newPatientExtract.PatientPID);

            _patientExtractRepository.Sync(newPatientExtract);

            var saved = _patientExtractRepository.Find(newPatientExtract.Id);
            Assert.IsNotNull(saved);
            Assert.AreEqual(saved.PatientPID, newPatientExtract.PatientPID);
            Assert.AreEqual(saved.Id, newPatientExtract.Id);
            Console.WriteLine(saved.PatientPID);
        }

        [Test]
        public void should_Clear_Manifest()
        {
            var manifest = new Manifest(_facilityA.Code);
            var currentPatients = _patients.Where(x => x.PatientPID > 1000);
            foreach (var p in currentPatients)
            {
                manifest.AddPatientPk(p.PatientPID);
            }

            _patientExtractRepository =new PatientExtractRepository(_dbcontext);
            _patientExtractRepository.ClearManifest(manifest);

            _patientExtractRepository=new PatientExtractRepository(new DwapiCentralContext());

            var cleanPatients = _patientExtractRepository.GetAllBy(x=>x.FacilityId==_facilityA.Id).ToList();

            Assert.IsTrue(cleanPatients.Count==4);
        }

        [TearDown]
        public void TearDown()
        {
            _dbcontext=new DwapiCentralContext();
            _dbcontext.Database.ExecuteSqlCommand($@"DELETE FROM [Facility] where Code={_facilityA.Code}");
        }
    }
}
