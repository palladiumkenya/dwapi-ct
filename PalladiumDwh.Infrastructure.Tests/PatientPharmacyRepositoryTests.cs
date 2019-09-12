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
    public class PatientPharmacyRepositoryTests
    {
        private DwapiCentralContext _context;
        private DwapiCentralContext _dbcontext;

        private List<Facility> _facilities;
        private List<MasterFacility> _masterFacilities;

        private IPatientExtractRepository _patientExtractRepository;


        private List<PatientExtract> _patients = new List<PatientExtract>();
        private List<PatientPharmacyProfile> _visitProfiles = new List<PatientPharmacyProfile>();
        private List<PatientPharmacyProfile> _newPharmacyProfiles = new List<PatientPharmacyProfile>();
        private List<PatientPharmacyProfile> _updatedPharmacyProfiles = new List<PatientPharmacyProfile>();

        private FacilityRepository _facilityRepository;
        private PatientPharmacyRepository _patientPharmacyRepository;
        private Facility _newFacility;
        private List<PatientExtract> _newFacilityPatients;
        private List<PatientPharmacyExtract> _newPharmacyExtracts = new List<PatientPharmacyExtract>();

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiCentralContext();
            _patients = new List<PatientExtract>();
            _visitProfiles = new List<PatientPharmacyProfile>();

            #region Test data
            //  facility
            _facilities = TestHelpers.GetTestData<Facility>(2).ToList();
            _facilities[0].Code = 100;
            _facilities[1].Code = 200;
            TestHelpers.CreateTestData(_context, _facilities);

            //  patient
            foreach (var facility in _facilities)
            {
                var patients = TestHelpers.GetTestPatientPharmacyData(facility, 2, 2).ToList();
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

            #region Test Pharmacy profiles

            var ids = _facilities.Select(x => x.Id).ToList();
            var facilities = _context.Facilities
                .Where(x => ids.Contains(x.Id))
                .Include(d => d.PatientExtracts.Select(v => v.PatientPharmacyExtracts));

            foreach (var facility in facilities)
            {
                foreach (var patientExtract in facility.PatientExtracts)
                {
                    var v = new PatientPharmacyProfile();
                    v.PatientInfo = patientExtract;
                    v.FacilityInfo = facility;
                    v.PharmacyExtracts =
                        new PatientPharmacyExtractDTO().GeneratePatientPharmacyExtractDtOs(
                            patientExtract.PatientPharmacyExtracts).ToList();
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
            _newFacilityPatients = TestHelpers.GetTestPatientPharmacyData(_newFacility, 2, 1).ToList();
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
                var v = new PatientPharmacyProfile();
                v.PatientInfo = patientExtract;
                v.FacilityInfo = _newFacility;
                v.PharmacyExtracts =
                    new PatientPharmacyExtractDTO().GeneratePatientPharmacyExtractDtOs(
                        patientExtract.PatientPharmacyExtracts).ToList();
                _newPharmacyProfiles.Add(v);
            }
            #endregion

            // UPDATE visits for exisiting patient

            #region Updated visits
            _updatedPharmacyProfiles = new List<PatientPharmacyProfile>();
            _updatedPharmacyProfiles.AddRange(_visitProfiles);
            _newPharmacyExtracts = TestHelpers.GetTestData<PatientPharmacyExtract>(2).ToList();
            foreach (var extract in _newPharmacyExtracts)
            {
                extract.PatientId = _patients[0].Id;
            }
            #endregion

            _facilityRepository = new FacilityRepository(_context);
            _patientExtractRepository = new PatientExtractRepository(_context);
            _patientPharmacyRepository = new PatientPharmacyRepository(_context);

        }

        [Test]
        public void should_Clear_Pharmacys()
        {
            var patientExtract = _patients.First();

            var visits = _context.PatientPharmacyExtracts.Where(x => x.PatientId.Equals(patientExtract.Id)).ToList();
            Assert.True(visits.Count > 0);

            _patientPharmacyRepository.ClearNew(patientExtract.Id);
            _patientPharmacyRepository = new PatientPharmacyRepository(_context);

            visits = _context.PatientPharmacyExtracts.Where(x => x.PatientId.Equals(patientExtract.Id)).ToList();
            Assert.True(visits.Count == 0);
        }
        [Test]
        public void should_Sync_With_Updated_Patients_Added()
        {
            var facility = _facilities[0];
            int pid = 1;
            var newPatients = TestHelpers.GetTestPatientPharmacyData(facility, 2, 3).ToList();
            foreach (var patientExtract in newPatients)
            {
                patientExtract.PatientCccNumber = $"10000/18/{pid}";
                facility.PatientExtracts.Add(patientExtract);
                pid++;
            }
            foreach (var patientExtract in facility.PatientExtracts)
            {
                var v = new PatientPharmacyProfile();
                v.PatientInfo = patientExtract;
                v.FacilityInfo = _facilities[0];
                v.PharmacyExtracts =
                    new PatientPharmacyExtractDTO().GeneratePatientPharmacyExtractDtOs(
                        patientExtract.PatientPharmacyExtracts).ToList();
                _visitProfiles.Add(v);
            }

            _patientPharmacyRepository.SyncNewPatients(_visitProfiles, _facilityRepository, new List<Guid>());
            _context = new DwapiCentralContext();
            var facilty = _context.Facilities.Where(x => x.Id == facility.Id)
                .Include(p => p.PatientExtracts.Select(v => v.PatientPharmacyExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First(x => x.Id == newPatients[0].Id);
            Assert.NotNull(patient);
            var visitis = patient.PatientPharmacyExtracts.Count;
            Assert.IsTrue(visitis == 3);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| Pharmacys:{visitis}");
        }
        [Test]
        public void should_Sync_With_Updated_Patients_Updated()
        {
            var patientInfo = _visitProfiles.Last().PatientInfo;
            patientInfo.MaritalStatus = "Married";
            patientInfo.PatientCccNumber = "15701-0001";

            _patientPharmacyRepository.SyncNewPatients(_visitProfiles, _facilityRepository, new List<Guid>());
            _context = new DwapiCentralContext();
            var facilty = _context.Facilities.Where(x => x.Id == patientInfo.FacilityId)
                .Include(p => p.PatientExtracts.Select(v => v.PatientPharmacyExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First(x => x.Id == patientInfo.Id);
            Assert.NotNull(patient);
            Assert.AreEqual("Married", patient.MaritalStatus);
            Assert.AreEqual("15701-0001", patient.PatientCccNumber);
            var visitis = patient.PatientPharmacyExtracts.Count;
            Assert.IsTrue(visitis > 0);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| Pharmacys:{visitis}");
        }
        [Test]
        public void should_Sync_New_Facilty_With_Patients()
        {
            _patientPharmacyRepository.SyncNewPatients(_newPharmacyProfiles, _facilityRepository, new List<Guid>());
            _context = new DwapiCentralContext();
            var facilty = _context.Facilities.Where(x => x.Code == _newFacility.Code)
                .Include(p => p.PatientExtracts.Select(v => v.PatientPharmacyExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First();
            Assert.NotNull(patient);
            var visitis = patient.PatientPharmacyExtracts.Count;
            Assert.IsTrue(visitis == 1);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| Pharmacys:{visitis}");
        }
        [Test]
        public void should_Sync_Patient_With_Updated_Pharmacys_Added()
        {
            _updatedPharmacyProfiles
                .First(x => x.PatientInfo.Id == _patients[0].Id)
                .PharmacyExtracts
                .AddRange(new PatientPharmacyExtractDTO().GeneratePatientPharmacyExtractDtOs(_newPharmacyExtracts).ToList());
            _patientPharmacyRepository.SyncNewPatients(_updatedPharmacyProfiles, _facilityRepository, new List<Guid>());
            _context = new DwapiCentralContext();

            var facilty = _context.Facilities.Where(x => x.Code == 100)
                .Include(p => p.PatientExtracts.Select(v => v.PatientPharmacyExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First(x => x.Id == _patients[0].Id);
            Assert.NotNull(patient);
            var visits = patient.PatientPharmacyExtracts.Count;
            Assert.IsTrue(visits == 4);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| Pharmacys:{visits}");
        }
        [Test]
        public void should_Sync_Patient_With_Updated_Pharmacys_Modified()
        {
            var visit = _visitProfiles.Last().PharmacyExtracts.First();

            _visitProfiles.Last().PharmacyExtracts.First().RegimenLine = "MAUN";
            _patientPharmacyRepository.SyncNewPatients(_visitProfiles, _facilityRepository, new List<Guid>());
            _context = new DwapiCentralContext();
            var patientExtract = _context.PatientExtracts.Where(x => x.Id == visit.PatientId).Include(v => v.PatientPharmacyExtracts).FirstOrDefault();
            Assert.NotNull(patientExtract);

            var visits = patientExtract.PatientPharmacyExtracts.FirstOrDefault(x => x.VisitID == visit.VisitID);
            Assert.NotNull(visits);
            Assert.AreEqual("MAUN", visits.RegimenLine);
        }
        [Test]
        public void should_Sync_Patient_With_Updated_Pharmacys_Deleted()
        {
            var visit = _visitProfiles.Last().PharmacyExtracts.First();

            _visitProfiles.Last().PharmacyExtracts.Remove(visit);
            _patientPharmacyRepository.SyncNewPatients(_visitProfiles, _facilityRepository, new List<Guid>());
            _context = new DwapiCentralContext();
            var patientExtract = _context.PatientExtracts.Where(x => x.Id == visit.PatientId).Include(v => v.PatientPharmacyExtracts).FirstOrDefault();
            Assert.NotNull(patientExtract);

            var visits = patientExtract.PatientPharmacyExtracts.FirstOrDefault(x => x.VisitID == visit.VisitID);
            Assert.Null(visits);
        }

        [TearDown]
        public void TearDown()
        {
            var deleteIds = new List<string>();

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
