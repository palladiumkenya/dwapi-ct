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
    public class PatientArtRepositoryTests
    {
        private DwapiCentralContext _context;
        private DwapiCentralContext _dbcontext;

        private List<Facility> _facilities;
        private List<MasterFacility> _masterFacilities;

        private IPatientExtractRepository _patientExtractRepository;

       
        private List<PatientExtract> _patients = new List<PatientExtract>();
        private List<PatientARTProfile> _visitProfiles=new List<PatientARTProfile>();
        private List<PatientARTProfile> _newVisitProfiles = new List<PatientARTProfile>();
        private List<PatientARTProfile> _updatedVisitProfiles = new List<PatientARTProfile>();

        private FacilityRepository _facilityRepository;
        private PatientArtExtractRepository _patientArtExtractRepository;
        private Facility _newFacility;
        private List<PatientExtract> _newFacilityPatients;
        private List<PatientArtExtract> _newArtExtracts = new List<PatientArtExtract>();

        [SetUp]
        public void SetUp()
        {
            _patients=new List<PatientExtract>();
            _context = new DwapiCentralContext();
            _visitProfiles=new List<PatientARTProfile>();

            #region Test data
            //  facility
            _facilities = TestHelpers.GetTestData<Facility>(2).ToList();
            _facilities[0].Code = 100;
            _facilities[1].Code = 200;
            TestHelpers.CreateTestData(_context, _facilities);
            
            //  patient
            foreach (var facility in _facilities)
            {
                var patients = TestHelpers.GetTestPatientARTData(facility, 3).ToList();
             
                _patients.AddRange(patients);
            }
            _patients.Last().PatientArtExtracts = new List<PatientArtExtract>();
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
                .Include(d => d.PatientExtracts.Select(v => v.PatientArtExtracts));

            foreach (var facility in facilities)
            {
                foreach (var patientExtract in facility.PatientExtracts)
                {
                    var v = new PatientARTProfile();
                    v.PatientInfo = patientExtract;
                    v.FacilityInfo = facility;
                    
                    v.ArtExtracts =
                        new PatientArtExtractDTO().GeneratePatientArtExtractDtOs(
                            patientExtract.PatientArtExtracts).ToList();
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
            _newFacilityPatients = TestHelpers.GetTestPatientARTData(_newFacility, 2).ToList();
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
                var v = new PatientARTProfile();
                v.PatientInfo = patientExtract;
                v.FacilityInfo = _newFacility;
                v.ArtExtracts =
                    new PatientArtExtractDTO().GeneratePatientArtExtractDtOs(
                        patientExtract.PatientArtExtracts).ToList();
                _newVisitProfiles.Add(v);
            }
            #endregion

            // UPDATE visits for exisiting patient
        
            #region Updated visits
            _updatedVisitProfiles = new List<PatientARTProfile>();
            _updatedVisitProfiles.AddRange(_visitProfiles);
            _newArtExtracts = TestHelpers.GetTestData<PatientArtExtract>(1).ToList();
            foreach (var extract in _newArtExtracts)
            {
                extract.PatientId = _patients[5].Id;
            }
            #endregion

            _facilityRepository = new FacilityRepository(_context);
            _patientExtractRepository = new PatientExtractRepository(_context);
            _patientArtExtractRepository = new PatientArtExtractRepository(_context);

        }

        [Test]
        public void should_Clear_Arts()
        {
            var patientExtract = _patients.First();

            var visits = _context.PatientArtExtracts.Where(x => x.PatientId.Equals(patientExtract.Id)).ToList();
            Assert.True(visits.Count>0);
            
            _patientArtExtractRepository.ClearNew(patientExtract.Id);
            _patientArtExtractRepository=new PatientArtExtractRepository(_context);

            visits = _context.PatientArtExtracts.Where(x => x.PatientId.Equals(patientExtract.Id)).ToList();
            Assert.True(visits.Count == 0);
        }
        [Test]
        public void should_Sync_With_Updated_Patients_Added()
        {
            var facility = _facilities[0];
            int pid = 1;
            var newPatients = TestHelpers.GetTestPatientARTData(facility, 1).ToList();
            foreach (var patientExtract in newPatients)
            {
                patientExtract.PatientCccNumber =$"10000/18/{pid}";
                facility.PatientExtracts.Add(patientExtract);
                pid++;
            }
            foreach (var patientExtract in facility.PatientExtracts)
            {
                var v = new PatientARTProfile();
                v.PatientInfo = patientExtract;
                v.FacilityInfo = _facilities[0];
                v.ArtExtracts =
                    new PatientArtExtractDTO().GeneratePatientArtExtractDtOs(
                        patientExtract.PatientArtExtracts).ToList();
                _visitProfiles.Add(v);
            }
            
            _patientArtExtractRepository.SyncNewPatients(_visitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var facilty = _context.Facilities.Where(x => x.Id == facility.Id)
                .Include(p => p.PatientExtracts.Select(v => v.PatientArtExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First(x=>x.Id==newPatients[0].Id);
            Assert.NotNull(patient);
            var visitis = patient.PatientArtExtracts.Count;
            Assert.IsTrue(visitis == 1);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| Visits:{visitis}");
        }
        [Test]
        public void should_Sync_With_Updated_Patients_Updated()
        {
            var patientInfo = _visitProfiles.First(x=>x.PatientInfo.Id==_patients[1].Id).PatientInfo;
            patientInfo.MaritalStatus = "Married";
            patientInfo.PatientCccNumber = "15701-0001";

            _patientArtExtractRepository.SyncNewPatients(_visitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var facilty = _context.Facilities.Where(x => x.Id == patientInfo.FacilityId)
                .Include(p => p.PatientExtracts.Select(v => v.PatientArtExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First(x => x.Id == patientInfo.Id);
            Assert.NotNull(patient);
            Assert.AreEqual("Married", patient.MaritalStatus);
            Assert.AreEqual("15701-0001", patient.PatientCccNumber);
            var visitis = patient.PatientArtExtracts.Count;
            Assert.IsTrue(visitis >0);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| Visits:{visitis}");
        }
        [Test]
        public void should_Sync_New_Facilty_With_Patients()
        {
            _patientArtExtractRepository.SyncNewPatients(_newVisitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var facilty = _context.Facilities.Where(x => x.Code == _newFacility.Code)
                .Include(p => p.PatientExtracts.Select(v => v.PatientArtExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First();
            Assert.NotNull(patient);
            var visitis = patient.PatientArtExtracts.Count;
            Assert.IsTrue(visitis == 1);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| Visits:{visitis}");
        }
        [Test]
        public void should_Sync_Patient_With_Updated_Arts_Added()
        {
            var vv = _updatedVisitProfiles.First(x => x.ArtExtracts.Count == 0);
            _newArtExtracts.First().PatientId = vv.PatientInfo.Id;

            vv.ArtExtracts
                .AddRange(new PatientArtExtractDTO().GeneratePatientArtExtractDtOs(_newArtExtracts).ToList());

            
            _patientArtExtractRepository.SyncNewPatients(_updatedVisitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();

            var facilty = _context.Facilities.Where(x => x.Id == vv.PatientInfo.FacilityId)
                .Include(p => p.PatientExtracts.Select(v => v.PatientArtExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First(x => x.Id == vv.PatientInfo.Id);
            Assert.NotNull(patient);
            var visits = patient.PatientArtExtracts.Count;
            Assert.IsTrue(visits == 1);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| Visits:{visits}");
        }
        [Test]
        public void should_Sync_Patient_With_Updated_Arts_Modified()
        {
            var visit = _visitProfiles.First().ArtExtracts.First();

            visit.LastRegimen = "MAUN";
            _patientArtExtractRepository.SyncNewPatients(_visitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var patientExtract = _context.PatientExtracts.Where(x => x.Id == visit.PatientId).Include(v=>v.PatientArtExtracts).FirstOrDefault();
            Assert.NotNull(patientExtract);
     
            var visits = patientExtract.PatientArtExtracts.FirstOrDefault();
            Assert.NotNull(visits);
            Assert.AreEqual("MAUN",visits.LastRegimen);
        }
        [Test]
        public void should_Sync_Patient_With_Updated_Arts_Deleted()
        {
            var visit = _visitProfiles.First();
            Assert.True(visit.ArtExtracts.Count>0);
           visit.ArtExtracts.Remove(visit.ArtExtracts.First());
            _patientArtExtractRepository.SyncNewPatients(_visitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var patientExtract = _context.PatientExtracts.Where(x => x.Id == visit.PatientInfo.Id).Include(v => v.PatientArtExtracts).FirstOrDefault();
            Assert.NotNull(patientExtract);

            var visits = patientExtract.PatientArtExtracts.FirstOrDefault();
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
