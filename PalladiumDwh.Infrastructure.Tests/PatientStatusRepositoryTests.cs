using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Infrastructure.Data.Repository;
using PalladiumDwh.Shared.Extentions;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;


namespace PalladiumDwh.Infrastructure.Tests
{
    [TestFixture]
    public class PatientStatusRepositoryTests
    {
        private DwapiCentralContext _context;
        private DwapiCentralContext _dbcontext;

        private List<Facility> _facilities;
        private List<MasterFacility> _masterFacilities;

        private IPatientExtractRepository _patientExtractRepository;

       
        private List<PatientExtract> _patients = new List<PatientExtract>();
        private List<PatientStatusProfile> _visitProfiles=new List<PatientStatusProfile>();
        private List<PatientStatusProfile> _newVisitProfiles = new List<PatientStatusProfile>();
        private List<PatientStatusProfile> _updatedVisitProfiles = new List<PatientStatusProfile>();

        private FacilityRepository _facilityRepository;
        private  PatientStatusRepository _patientStatusRepository;
        private Facility _newFacility;
        private List<PatientExtract> _newFacilityPatients;
        private List<PatientStatusExtract> _newStatusExtracts = new List<PatientStatusExtract>();

        [SetUp]
        public void SetUp()
        {
            _patients=new List<PatientExtract>();
            _context = new DwapiCentralContext();
            _visitProfiles=new List<PatientStatusProfile>();

            #region Test data
            //  facility
            _facilities = TestHelpers.GetTestData<Facility>(2).ToList();
            _facilities[0].Code = 100;
            _facilities[1].Code = 200;
            TestHelpers.CreateTestData(_context, _facilities);
            
            //  patient
            foreach (var facility in _facilities)
            {
                var patients = TestHelpers.GetTestPatientStatusData(facility, 3).ToList();
             
                _patients.AddRange(patients);
            }
            _patients.Last().PatientStatusExtracts = new List<PatientStatusExtract>();
            int pid = 1000;
            foreach (var p in _patients)
            {
                p.PatientPID = pid;
                pid++;
            }
            TestHelpers.CreateTestData(_context, _patients);
            #endregion

            #region Test Visit profiles

            var ids = _facilities.Select(x => x.Id).ToList();
            var facilities = _context.Facilities
                .Where(x => ids.Contains(x.Id))
                .Include(d => d.PatientExtracts.Select(v => v.PatientStatusExtracts));

            foreach (var facility in facilities)
            {
                foreach (var patientExtract in facility.PatientExtracts)
                {
                    var v = new PatientStatusProfile();
                    v.PatientInfo = patientExtract;
                    v.FacilityInfo = facility;
                    
                    v.StatusExtracts =
                        new PatientStatusExtractDTO().GeneratePatientStatusExtractDtOs(
                            patientExtract.PatientStatusExtracts).ToList();
                    _visitProfiles.Add(v);
                }
            }


            #endregion

            #region NEW Test data
            //  NEW facility
            _newFacility = TestHelpers.GetTestData<Facility>(1).ToList().First();
            _newFacility.Name = "Migori County Hospital";
            _newFacility.Code = 300;
            _newFacility.Id = new Guid("73F27F28-B38C-4FE3-9234-A865008C9717");

            //  NEW patients
            _newFacilityPatients = TestHelpers.GetTestPatientStatusData(_newFacility, 2).ToList();
            pid = 2000;
            foreach (var p in _newFacilityPatients)
            {
                p.PatientPID = pid;
                pid++;
            }
            foreach (var newFacilityPatient in _newFacilityPatients)
            {
                _newFacility.PatientExtracts.Add(newFacilityPatient);
            }
            #endregion

            #region NEW Test Data visit profiles
            foreach (var patientExtract in _newFacility.PatientExtracts)
            {
                var v = new PatientStatusProfile();
                v.PatientInfo = patientExtract;
                v.FacilityInfo = _newFacility;
                v.StatusExtracts =
                    new PatientStatusExtractDTO().GeneratePatientStatusExtractDtOs(
                        patientExtract.PatientStatusExtracts).ToList();
                _newVisitProfiles.Add(v);
            }
            #endregion

            // UPDATE visits for exisiting patient
        
            #region Updated visits
            _updatedVisitProfiles = new List<PatientStatusProfile>();
            _updatedVisitProfiles.AddRange(_visitProfiles);
            _newStatusExtracts = TestHelpers.GetTestData<PatientStatusExtract>(1).ToList();
            foreach (var extract in _newStatusExtracts)
            {
                extract.PatientId = _patients[5].Id;
            }
            #endregion

            _facilityRepository = new FacilityRepository(_context);
            _patientExtractRepository = new PatientExtractRepository(_context);
            _patientStatusRepository = new PatientStatusRepository(_context);

        }

        [Test]
        public void should_Clear_Statuss()
        {
            var patientExtract = _patients.First();

            var visits = _context.PatientStatusExtracts.Where(x => x.PatientId.Equals(patientExtract.Id)).ToList();
            Assert.True(visits.Count>0);
            
            _patientStatusRepository.ClearNew(patientExtract.Id);
            _patientStatusRepository=new PatientStatusRepository(_context);

            visits = _context.PatientStatusExtracts.Where(x => x.PatientId.Equals(patientExtract.Id)).ToList();
            Assert.True(visits.Count == 0);
        }
        [Test]
        public void should_Sync_With_Updated_Patients_Added()
        {
            var facility = _facilities[0];
            int pid = 1;
            var newPatients = TestHelpers.GetTestPatientStatusData(facility, 1).ToList();
            foreach (var patientExtract in newPatients)
            {
                patientExtract.PatientCccNumber =$"10000/18/{pid}";
                facility.PatientExtracts.Add(patientExtract);
                pid++;
            }
            foreach (var patientExtract in facility.PatientExtracts)
            {
                var v = new PatientStatusProfile();
                v.PatientInfo = patientExtract;
                v.FacilityInfo = _facilities[0];
                v.StatusExtracts =
                    new PatientStatusExtractDTO().GeneratePatientStatusExtractDtOs(
                        patientExtract.PatientStatusExtracts).ToList();
                _visitProfiles.Add(v);
            }
            
            _patientStatusRepository.SyncNewPatients(_visitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var facilty = _context.Facilities.Where(x => x.Id == facility.Id)
                .Include(p => p.PatientExtracts.Select(v => v.PatientStatusExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First(x=>x.Id==newPatients[0].Id);
            Assert.NotNull(patient);
            var visitis = patient.PatientStatusExtracts.Count;
            Assert.IsTrue(visitis == 1);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| Visits:{visitis}");
        }
        [Test]
        public void should_Sync_With_Updated_Patients_Updated()
        {
            var patientInfo = _visitProfiles.First(x=>x.PatientInfo.Id==_patients[1].Id).PatientInfo;
            patientInfo.MaritalStatus = "Married";
            patientInfo.PatientCccNumber = "15701-0001";

            _patientStatusRepository.SyncNewPatients(_visitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var facilty = _context.Facilities.Where(x => x.Id == patientInfo.FacilityId)
                .Include(p => p.PatientExtracts.Select(v => v.PatientStatusExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First(x => x.Id == patientInfo.Id);
            Assert.NotNull(patient);
            Assert.AreEqual("Married", patient.MaritalStatus);
            Assert.AreEqual("15701-0001", patient.PatientCccNumber);
            var visitis = patient.PatientStatusExtracts.Count;
            Assert.IsTrue(visitis >0);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| Visits:{visitis}");
        }
        [Test]
        public void should_Sync_New_Facilty_With_Patients()
        {
            _patientStatusRepository.SyncNewPatients(_newVisitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var facilty = _context.Facilities.Where(x => x.Code == _newFacility.Code)
                .Include(p => p.PatientExtracts.Select(v => v.PatientStatusExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First();
            Assert.NotNull(patient);
            var visitis = patient.PatientStatusExtracts.Count;
            Assert.IsTrue(visitis == 1);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| Visits:{visitis}");
        }
        [Test]
        public void should_Sync_Patient_With_Updated_Statuss_Added()
        {
            var vv = _updatedVisitProfiles.First(x => x.StatusExtracts.Count == 0);
            _newStatusExtracts.First().PatientId = vv.PatientInfo.Id;

            vv.StatusExtracts
                .AddRange(new PatientStatusExtractDTO().GeneratePatientStatusExtractDtOs(_newStatusExtracts).ToList());

            
            _patientStatusRepository.SyncNewPatients(_updatedVisitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();

            var facilty = _context.Facilities.Where(x => x.Id == vv.PatientInfo.FacilityId)
                .Include(p => p.PatientExtracts.Select(v => v.PatientStatusExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First(x => x.Id == vv.PatientInfo.Id);
            Assert.NotNull(patient);
            var visits = patient.PatientStatusExtracts.Count;
            Assert.IsTrue(visits == 1);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| Visits:{visits}");
        }
        [Test]
        public void should_Sync_Patient_With_Updated_Statuss_Modified()
        {
            var visit = _visitProfiles.First().StatusExtracts.First();

            visit.ExitReason = "MAUN";
            _patientStatusRepository.SyncNewPatients(_visitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var patientExtract = _context.PatientExtracts.Where(x => x.Id == visit.PatientId).Include(v=>v.PatientStatusExtracts).FirstOrDefault();
            Assert.NotNull(patientExtract);
     
            var visits = patientExtract.PatientStatusExtracts.FirstOrDefault();
            Assert.NotNull(visits);
            Assert.AreEqual("MAUN",visits.ExitReason);
        }
        [Test]
        public void should_Sync_Patient_With_Updated_Statuss_Deleted()
        {
            var visit = _visitProfiles.First();
            Assert.True(visit.StatusExtracts.Count>0);
           visit.StatusExtracts.Remove(visit.StatusExtracts.First());
            _patientStatusRepository.SyncNewPatients(_visitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var patientExtract = _context.PatientExtracts.Where(x => x.Id == visit.PatientInfo.Id).Include(v => v.PatientStatusExtracts).FirstOrDefault();
            Assert.NotNull(patientExtract);

            var visits = patientExtract.PatientStatusExtracts.FirstOrDefault();
            Assert.Null(visits);
        }
        
        [TearDown]
        public void TearDown()
        {
            var deleteIds=new List<string>();

            foreach (var facility in _facilities)
            {
                deleteIds.Add(facility.Id.ToString());
            }

            deleteIds.Add(_newFacility.Id.ToString());

            if (deleteIds.Count > 0)
            {
                var ids = new List<string>();
                foreach (var deleteId in deleteIds)
                {
                    ids.Add($"'{deleteId}'");
                }
                _context.Database.ExecuteSqlCommand($"DELETE FROM Facility WHERE Id IN ({string.Join(",", ids)})");
            }
        }
    }
}
