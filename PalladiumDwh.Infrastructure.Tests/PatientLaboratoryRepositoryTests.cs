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
    public class PatientLabRepositoryTests
    {
        private DwapiCentralContext _context;
        private DwapiCentralContext _dbcontext;

        private List<Facility> _facilities;
        private List<MasterFacility> _masterFacilities;

        private IPatientExtractRepository _patientExtractRepository;

        
        private List<PatientExtract> _patients = new List<PatientExtract>();
        private List<PatientLabProfile> _visitProfiles = new List<PatientLabProfile>();
        private List<PatientLabProfile> _newLaboratoryProfiles = new List<PatientLabProfile>();
        private List<PatientLabProfile> _updatedLaboratoryProfiles = new List<PatientLabProfile>();

        private FacilityRepository _facilityRepository;
        private PatientLabRepository _patientLaboratoryRepository;
        private Facility _newFacility;
        private List<PatientExtract> _newFacilityPatients;
        private List<PatientLaboratoryExtract> _newLaboratoryExtracts = new List<PatientLaboratoryExtract>();

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiCentralContext();
            _patients = new List<PatientExtract>();
            _visitProfiles = new List<PatientLabProfile>();

            #region Test data
            //  facility
            _facilities = TestHelpers.GetTestData<Facility>(2).ToList();
            _facilities[0].Code = 100;
            _facilities[1].Code = 200;
            TestHelpers.CreateTestData(_context, _facilities);

            //  patient
            foreach (var facility in _facilities)
            {
                var patients = TestHelpers.GetTestPatientLaboratoryData(facility, 2, 2).ToList();
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

            #region Test Laboratory profiles

            var ids = _facilities.Select(x => x.Id).ToList();
            var facilities = _context.Facilities
                .Where(x => ids.Contains(x.Id))
                .Include(d => d.PatientExtracts.Select(v => v.PatientLaboratoryExtracts));

            foreach (var facility in facilities)
            {
                foreach (var patientExtract in facility.PatientExtracts)
                {
                    var v = new PatientLabProfile();
                    v.PatientInfo = patientExtract;
                    v.FacilityInfo = facility;
                    v.LaboratoryExtracts =
                        new PatientLaboratoryExtractDTO().GenerateLaboratoryExtractDtOs(
                            patientExtract.PatientLaboratoryExtracts).ToList();
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
            _newFacilityPatients = TestHelpers.GetTestPatientLaboratoryData(_newFacility, 2, 1).ToList();
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
                var v = new PatientLabProfile();
                v.PatientInfo = patientExtract;
                v.FacilityInfo = _newFacility;
                v.LaboratoryExtracts =
                    new PatientLaboratoryExtractDTO().GenerateLaboratoryExtractDtOs(
                        patientExtract.PatientLaboratoryExtracts).ToList();
                _newLaboratoryProfiles.Add(v);
            }
            #endregion

            // UPDATE visits for exisiting patient

            #region Updated visits
            _updatedLaboratoryProfiles = new List<PatientLabProfile>();
            _updatedLaboratoryProfiles.AddRange(_visitProfiles);
            _newLaboratoryExtracts = TestHelpers.GetTestData<PatientLaboratoryExtract>(2).ToList();
            foreach (var extract in _newLaboratoryExtracts)
            {
                extract.PatientId = _patients[0].Id;
            }
            #endregion

            _facilityRepository = new FacilityRepository(_context);
            _patientExtractRepository = new PatientExtractRepository(_context);
            _patientLaboratoryRepository = new PatientLabRepository(_context);

        }

        [Test]
        public void should_Clear_Laboratorys()
        {
            var patientExtract = _patients.First();

            var visits = _context.PatientLaboratoryExtracts.Where(x => x.PatientId.Equals(patientExtract.Id)).ToList();
            Assert.True(visits.Count > 0);

            _patientLaboratoryRepository.ClearNew(patientExtract.Id);
            _patientLaboratoryRepository = new PatientLabRepository(_context);

            visits = _context.PatientLaboratoryExtracts.Where(x => x.PatientId.Equals(patientExtract.Id)).ToList();
            Assert.True(visits.Count == 0);
        }
        [Test]
        public void should_Sync_With_Updated_Patients_Added()
        {
            var facility = _facilities[0];
            int pid = 1;
            var newPatients = TestHelpers.GetTestPatientLaboratoryData(facility, 2, 3).ToList();
            foreach (var patientExtract in newPatients)
            {
                patientExtract.PatientCccNumber = $"10000/18/{pid}";
                facility.PatientExtracts.Add(patientExtract);
                pid++;
            }
            foreach (var patientExtract in facility.PatientExtracts)
            {
                var v = new PatientLabProfile();
                v.PatientInfo = patientExtract;
                v.FacilityInfo = _facilities[0];
                v.LaboratoryExtracts =
                    new PatientLaboratoryExtractDTO().GenerateLaboratoryExtractDtOs(
                        patientExtract.PatientLaboratoryExtracts).ToList();
                _visitProfiles.Add(v);
            }

            _patientLaboratoryRepository.SyncNewPatients(_visitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var facilty = _context.Facilities.Where(x => x.Id == facility.Id)
                .Include(p => p.PatientExtracts.Select(v => v.PatientLaboratoryExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First(x => x.Id == newPatients[0].Id);
            Assert.NotNull(patient);
            var visitis = patient.PatientLaboratoryExtracts.Count;
            Assert.IsTrue(visitis == 3);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| Laboratorys:{visitis}");
        }
        [Test]
        public void should_Sync_With_Updated_Patients_Updated()
        {
            var patientInfo = _visitProfiles.Last().PatientInfo;
            patientInfo.MaritalStatus = "Married";
            patientInfo.PatientCccNumber = "15701-0001";

            _patientLaboratoryRepository.SyncNewPatients(_visitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var facilty = _context.Facilities.Where(x => x.Id == patientInfo.FacilityId)
                .Include(p => p.PatientExtracts.Select(v => v.PatientLaboratoryExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First(x => x.Id == patientInfo.Id);
            Assert.NotNull(patient);
            Assert.AreEqual("Married", patient.MaritalStatus);
            Assert.AreEqual("15701-0001", patient.PatientCccNumber);
            var visitis = patient.PatientLaboratoryExtracts.Count;
            Assert.IsTrue(visitis > 0);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| Laboratorys:{visitis}");
        }
        [Test]
        public void should_Sync_New_Facilty_With_Patients()
        {
            _patientLaboratoryRepository.SyncNewPatients(_newLaboratoryProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var facilty = _context.Facilities.Where(x => x.Code == _newFacility.Code)
                .Include(p => p.PatientExtracts.Select(v => v.PatientLaboratoryExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First();
            Assert.NotNull(patient);
            var visitis = patient.PatientLaboratoryExtracts.Count;
            Assert.IsTrue(visitis == 1);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| Laboratorys:{visitis}");
        }
        [Test]
        public void should_Sync_Patient_With_Updated_Laboratorys_Added()
        {
            _updatedLaboratoryProfiles
                .First(x => x.PatientInfo.Id == _patients[0].Id)
                .LaboratoryExtracts
                .AddRange(new PatientLaboratoryExtractDTO().GenerateLaboratoryExtractDtOs(_newLaboratoryExtracts).ToList());
            _patientLaboratoryRepository.SyncNewPatients(_updatedLaboratoryProfiles, _facilityRepository);
            _context = new DwapiCentralContext();

            var facilty = _context.Facilities.Where(x => x.Code == 100)
                .Include(p => p.PatientExtracts.Select(v => v.PatientLaboratoryExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First(x => x.Id == _patients[0].Id);
            Assert.NotNull(patient);
            var visits = patient.PatientLaboratoryExtracts.Count;
            Assert.IsTrue(visits == 4);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| Laboratorys:{visits}");
        }
        [Test]
        public void should_Sync_Patient_With_Updated_Laboratorys_Modified()
        {
            var visit = _visitProfiles.Last().LaboratoryExtracts.First();

            _visitProfiles.Last().LaboratoryExtracts.First().TestName = "MAUN";
            _patientLaboratoryRepository.SyncNewPatients(_visitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var patientExtract = _context.PatientExtracts.Where(x => x.Id == visit.PatientId).Include(v => v.PatientLaboratoryExtracts).FirstOrDefault();
            Assert.NotNull(patientExtract);

            var visits = patientExtract.PatientLaboratoryExtracts.FirstOrDefault(x => x.VisitId == visit.VisitId);
            Assert.NotNull(visits);
            Assert.AreEqual("MAUN", visits.TestName);
        }
        [Test]
        public void should_Sync_Patient_With_Updated_Laboratorys_Deleted()
        {
            var visit = _visitProfiles.Last().LaboratoryExtracts.First();

            _visitProfiles.Last().LaboratoryExtracts.Remove(visit);
            _patientLaboratoryRepository.SyncNewPatients(_visitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var patientExtract = _context.PatientExtracts.Where(x => x.Id == visit.PatientId).Include(v => v.PatientLaboratoryExtracts).FirstOrDefault();
            Assert.NotNull(patientExtract);

            var visits = patientExtract.PatientLaboratoryExtracts.FirstOrDefault(x => x.VisitId == visit.VisitId);
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
