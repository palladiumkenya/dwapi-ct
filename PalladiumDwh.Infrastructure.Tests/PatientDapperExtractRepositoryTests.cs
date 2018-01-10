using System;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Infrastructure.Data.Repository;
using PalladiumDwh.Shared.Extentions;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;


namespace PalladiumDwh.Infrastructure.Tests
{
    [TestFixture]
    public class PatientDapperExtractRepositoryTests 
    {
        private DwapiCentralContext _context;
        private DwapiCentralContext _dbcontext;

        private List<Facility> _facilities;
        private List<MasterFacility> _masterFacilities;

        private IPatientExtractRepository _patientExtractRepository;

        private Facility _facilityA;
        private List<PatientExtract> _patients;

        [SetUp]
        public void SetUp()
        {
            _dbcontext = new DwapiCentralContext();
            

            _context = new DwapiCentralContext(Effort.DbConnectionFactory.CreateTransient(), true);

            _masterFacilities = Builder<MasterFacility>.CreateListOfSize(5).Build().ToList();
            int mfl = 100;
            foreach (var f in _masterFacilities)
            {
                f.Code = mfl;
                mfl++;
            }
            _context.MasterFacilities.AddRange(_masterFacilities);
            _context.SaveChanges();

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
        public void should_Clear_Manifest()
        {
            var manifest = new Manifest(_facilityA.Code);
            var currentPatients = _patients.Where(x => x.PatientPID > 1000);
            foreach (var p in currentPatients)
            {
                manifest.AddPatientPk(p.PatientPID);
            }

            _patientExtractRepository = new PatientExtractRepository(_dbcontext);
            _patientExtractRepository.ClearManifest(manifest);

            _patientExtractRepository = new PatientExtractRepository(new DwapiCentralContext());

            var cleanPatients = _patientExtractRepository.GetAllBy(x => x.FacilityId == _facilityA.Id).ToList();

            Assert.IsTrue(cleanPatients.Count == 4);
        }

        [Test]
        public void should_GetPatientByIds()
        {

            _patientExtractRepository = new PatientExtractRepository(_dbcontext);
            var id = _patientExtractRepository.GetPatientByIds(_facilityA.Id,1001);
            Assert.False(id.IsNullOrEmpty());
            Console.WriteLine($"Patien ID:{id}");
        }



        [Test]
        public void should_Sync_New()
        {
            _patientExtractRepository = new PatientExtractRepository(_dbcontext);

            var patientExtract = Builder<PatientExtract>.CreateNew().Build();
            patientExtract.PatientPID = DateTime.UtcNow.Millisecond;
            patientExtract.FacilityId = _facilityA.Id;
            

            var savedId= _patientExtractRepository.SyncNew(patientExtract);
            Assert.False(savedId.IsNullOrEmpty());

            _patientExtractRepository = new PatientExtractRepository(_dbcontext);
            var saved = _patientExtractRepository.Find(patientExtract.Id);
            Assert.IsNotNull(saved);
            Assert.AreEqual(patientExtract.Id,saved.Id);
            Assert.AreEqual(patientExtract.PatientPID,saved.PatientPID);
            Console.WriteLine($"NEW:{saved.PatientPID}  | {saved.Id}");
        }

        [Test]
        public void should_Sync_Exisitng()
        {
            var patientExtract = _patients.Last();
            string old = patientExtract.MaritalStatus;
            var newPatientExtract = Builder<PatientExtract>.CreateNew().Build();
            newPatientExtract.PatientPID = patientExtract.PatientPID;
            newPatientExtract.FacilityId = patientExtract.FacilityId;
            newPatientExtract.MaritalStatus = "divorced";
            _patientExtractRepository = new PatientExtractRepository(_dbcontext);

            _patientExtractRepository.SyncNew(newPatientExtract);

            
            var saved = _patientExtractRepository.FindBy(patientExtract.Id);
            Assert.IsNotNull(saved);
            Assert.AreEqual(newPatientExtract.MaritalStatus, saved.MaritalStatus);
            Assert.AreEqual(newPatientExtract.Id,saved.Id);
            Assert.AreEqual(newPatientExtract.FacilityId, saved.FacilityId);
            Assert.AreEqual(newPatientExtract.PatientPID, saved.PatientPID);

            Console.WriteLine($"{saved.PatientPID} updates Martial Status:{saved.MaritalStatus} old:{old}");
        }

        [Test]
        public void should_Sync_Exisitng_New_Guid()
        {
            var patientExtract = _patients.Last();
            string old = patientExtract.MaritalStatus;
            patientExtract.MaritalStatus = "divorced";


            _patientExtractRepository.SyncNew(patientExtract);
            _patientExtractRepository = new PatientExtractRepository(_dbcontext);
            var saved = _patientExtractRepository.Find(patientExtract.Id);
            Assert.IsNotNull(saved);
            Assert.AreEqual(patientExtract.MaritalStatus, saved.MaritalStatus);
            Assert.AreEqual(patientExtract.Id, saved.Id);
            Assert.AreEqual(patientExtract.FacilityId, saved.FacilityId);
            Assert.AreEqual(patientExtract.PatientPID, saved.PatientPID);

            Console.WriteLine($"{saved.PatientPID} updates Martial Status:{saved.MaritalStatus} old:{old}");
        }


        [Test]
        public void should_Verify_MasterFacility()
        {
            int code = _masterFacilities.Last().Code;
            var mflsite = _patientExtractRepository.VerifyFacility(code).Result;
            Assert.IsNotNull(mflsite);
            Console.WriteLine(mflsite);

            var nonMflsite = _patientExtractRepository.VerifyFacility(-1100).Result;
            Assert.IsNull(nonMflsite);
            

        }

        [Test]
        public void should_Verify_Old_MasterFacility()
        {
            int code = _masterFacilities.Last().Code;
            code += 64800000;
            var mflsite = _patientExtractRepository.VerifyFacility(code).Result;
            Assert.IsNotNull(mflsite);
            Console.WriteLine(mflsite);

            code = _masterFacilities.Last().Code;
            code += 25400000;
            mflsite = _patientExtractRepository.VerifyFacility(code).Result;
            Assert.IsNotNull(mflsite);
            Console.WriteLine(mflsite);

            var nonMflsite = _patientExtractRepository.VerifyFacility(-1100).Result;
            Assert.IsNull(nonMflsite);


        }


        [TearDown]
        public void TearDown()
        {
            _dbcontext=new DwapiCentralContext();
            _dbcontext.Database.ExecuteSqlCommand($@"DELETE FROM [Facility] where Code={_facilityA.Code}");
        }
    }
}
