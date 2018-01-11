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
    public class PatientVisitRepositoryTests
    {
        private DwapiCentralContext _context;
        private DwapiCentralContext _dbcontext;

        private List<Facility> _facilities;
        private List<MasterFacility> _masterFacilities;

        private IPatientExtractRepository _patientExtractRepository;

       
        private List<PatientExtract> _patients = new List<PatientExtract>();
        private List<PatientVisitProfile> _visitProfiles=new List<PatientVisitProfile>();
        private List<PatientVisitProfile> _newVisitProfiles = new List<PatientVisitProfile>();
        private List<PatientVisitProfile> _updatedVisitProfiles = new List<PatientVisitProfile>();

        private FacilityRepository _facilityRepository;
        private PatientVisitRepository _patientVisitRepository;
        private Facility _newFacility;
        private List<PatientExtract> _newFacilityPatients;
        private List<PatientVisitExtract> _newVisitExtracts = new List<PatientVisitExtract>();

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiCentralContext();

            #region Test data
            //  facility
            _facilities = TestHelpers.GetTestData<Facility>(2).ToList();
            _facilities[0].Code = 100;
            _facilities[1].Code = 200;
            TestHelpers.CreateTestData(_context, _facilities);
            
            //  patient
            foreach (var facility in _facilities)
            {
                var patients = TestHelpers.GetTestPatientVisitsData(facility, 2, 2).ToList();
                _patients.AddRange(patients);
            }
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
                .Include(d => d.PatientExtracts.Select(v => v.PatientVisitExtracts));

            foreach (var facility in facilities)
            {
                foreach (var patientExtract in facility.PatientExtracts)
                {
                    var v = new PatientVisitProfile();
                    v.PatientInfo = patientExtract;
                    v.FacilityInfo = facility;
                    v.VisitExtracts =
                        new PatientVisitExtractDTO().GeneratePatientVisitExtractDtOs(
                            patientExtract.PatientVisitExtracts).ToList();
                    _visitProfiles.Add(v);
                }
            }


            #endregion

            #region NEW Test data
            //  NEW facility
            _newFacility = TestHelpers.GetTestData<Facility>(1).ToList().First();
            _newFacility.Name = "Migori County Hospital";
            _newFacility.Code = 300;

            //  NEW patients
            _newFacilityPatients = TestHelpers.GetTestPatientVisitsData(_newFacility, 2, 1).ToList();
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
                var v = new PatientVisitProfile();
                v.PatientInfo = patientExtract;
                v.FacilityInfo = _newFacility;
                v.VisitExtracts =
                    new PatientVisitExtractDTO().GeneratePatientVisitExtractDtOs(
                        patientExtract.PatientVisitExtracts).ToList();
                _newVisitProfiles.Add(v);
            }
            #endregion

            // UPDATE visits for exisiting patient
        
            #region Updated visits
            _updatedVisitProfiles = new List<PatientVisitProfile>();
            _updatedVisitProfiles.AddRange(_visitProfiles);
            _newVisitExtracts = TestHelpers.GetTestData<PatientVisitExtract>(2).ToList();
            foreach (var extract in _newVisitExtracts)
            {
                extract.PatientId = _patients[0].Id;
            }
            #endregion

            _facilityRepository = new FacilityRepository(_context);
            _patientExtractRepository = new PatientExtractRepository(_context);
            _patientVisitRepository = new PatientVisitRepository(_context);

        }

        [Test]
        public void should_Clear_Visits()
        {
            var patientExtract = _patients.First();

            var visits = _context.PatientVisitExtracts.Where(x => x.PatientId.Equals(patientExtract.Id)).ToList();
            Assert.True(visits.Count>0);
            
            _patientVisitRepository.ClearNew(patientExtract.Id);
            _patientVisitRepository=new PatientVisitRepository(_context);

            visits = _context.PatientVisitExtracts.Where(x => x.PatientId.Equals(patientExtract.Id)).ToList();
            Assert.True(visits.Count == 0);
        }
        [Test]
        public void should_Sync_With_Updated_Patients_Added()
        {
            var facility = _facilities[0];
            int pid = 1;
            var newPatients = TestHelpers.GetTestPatientVisitsData(facility, 2, 3).ToList();
            foreach (var patientExtract in newPatients)
            {
                patientExtract.PatientCccNumber =$"10000/18/{pid}";
                facility.PatientExtracts.Add(patientExtract);
                pid++;
            }
            foreach (var patientExtract in facility.PatientExtracts)
            {
                var v = new PatientVisitProfile();
                v.PatientInfo = patientExtract;
                v.FacilityInfo = _facilities[0];
                v.VisitExtracts =
                    new PatientVisitExtractDTO().GeneratePatientVisitExtractDtOs(
                        patientExtract.PatientVisitExtracts).ToList();
                _visitProfiles.Add(v);
            }
            
            _patientVisitRepository.SyncNewPatients(_visitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var facilty = _context.Facilities.Where(x => x.Id == facility.Id)
                .Include(p => p.PatientExtracts.Select(v => v.PatientVisitExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First(x=>x.Id==newPatients[0].Id);
            Assert.NotNull(patient);
            var visitis = patient.PatientVisitExtracts.Count;
            Assert.IsTrue(visitis == 3);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| Visits:{visitis}");
        }
        [Test]
        public void should_Sync_With_Updated_Patients_Updated()
        {
            var patientInfo = _visitProfiles.Last().PatientInfo;
            patientInfo.MaritalStatus = "Married";
            patientInfo.PatientCccNumber = "15701-0001";

            _patientVisitRepository.SyncNewPatients(_visitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var facilty = _context.Facilities.Where(x => x.Id == patientInfo.FacilityId)
                .Include(p => p.PatientExtracts.Select(v => v.PatientVisitExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First(x => x.Id == patientInfo.Id);
            Assert.NotNull(patient);
            Assert.AreEqual("Married", patient.MaritalStatus);
            Assert.AreEqual("15701-0001", patient.PatientCccNumber);
            var visitis = patient.PatientVisitExtracts.Count;
            Assert.IsTrue(visitis >0);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| Visits:{visitis}");
        }
        [Test]
        public void should_Sync_New_Facilty_With_Patients()
        {
            _patientVisitRepository.SyncNewPatients(_newVisitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var facilty = _context.Facilities.Where(x => x.Code == _newFacility.Code)
                .Include(p => p.PatientExtracts.Select(v => v.PatientVisitExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First();
            Assert.NotNull(patient);
            var visitis = patient.PatientVisitExtracts.Count;
            Assert.IsTrue(visitis == 1);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| Visits:{visitis}");
        }
        [Test]
        public void should_Sync_Patient_With_Updated_Visits_Added()
        {
            _updatedVisitProfiles
                .First(x => x.PatientInfo.Id == _patients[0].Id)
                .VisitExtracts
                .AddRange(new PatientVisitExtractDTO().GeneratePatientVisitExtractDtOs(_newVisitExtracts).ToList());
            _patientVisitRepository.SyncNewPatients(_updatedVisitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();

            var facilty = _context.Facilities.Where(x => x.Code == 100)
                .Include(p => p.PatientExtracts.Select(v => v.PatientVisitExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First(x => x.Id == _patients[0].Id);
            Assert.NotNull(patient);
            var visits = patient.PatientVisitExtracts.Count;
            Assert.IsTrue(visits == 4);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| Visits:{visits}");
        }
        [Test]
        public void should_Sync_Patient_With_Updated_Visits_Modified()
        {
            var visit = _visitProfiles.Last().VisitExtracts.First();

            _visitProfiles.Last().VisitExtracts.First().Service = "MAUN";
            _patientVisitRepository.SyncNewPatients(_visitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var patientExtract = _context.PatientExtracts.Where(x => x.Id == visit.PatientId).Include(v=>v.PatientVisitExtracts).FirstOrDefault();
            Assert.NotNull(patientExtract);
     
            var visits = patientExtract.PatientVisitExtracts.FirstOrDefault(x=>x.VisitId==visit.VisitId);
            Assert.NotNull(visits);
            Assert.AreEqual("MAUN",visits.Service);
        }
        [Test]
        public void should_Sync_Patient_With_Updated_Visits_Deleted()
        {
            var visit = _visitProfiles.Last().VisitExtracts.First();

            _visitProfiles.Last().VisitExtracts.Remove(visit);
            _patientVisitRepository.SyncNewPatients(_visitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var patientExtract = _context.PatientExtracts.Where(x => x.Id == visit.PatientId).Include(v => v.PatientVisitExtracts).FirstOrDefault();
            Assert.NotNull(patientExtract);

            var visits = patientExtract.PatientVisitExtracts.FirstOrDefault(x => x.VisitId == visit.VisitId);
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
