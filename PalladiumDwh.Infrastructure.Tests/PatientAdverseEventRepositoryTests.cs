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
    public class PatientAdverseEventRepositoryTests
    {
        private DwapiCentralContext _context;
        private DwapiCentralContext _dbcontext;

        private List<Facility> _facilities;
        private List<MasterFacility> _masterFacilities;

        private IPatientExtractRepository _patientExtractRepository;

       
        private List<PatientExtract> _patients = new List<PatientExtract>();
        private List<PatientAdverseEventProfile> _AdverseEventProfiles=new List<PatientAdverseEventProfile>();
        private List<PatientAdverseEventProfile> _newAdverseEventProfiles = new List<PatientAdverseEventProfile>();
        private List<PatientAdverseEventProfile> _updatedAdverseEventProfiles = new List<PatientAdverseEventProfile>();

        private FacilityRepository _facilityRepository;
        private PatientAdverseEventRepository _patientAdverseEventRepository;
        private Facility _newFacility;
        private List<PatientExtract> _newFacilityPatients;
        private List<PatientAdverseEventExtract> _newAdverseEventExtracts = new List<PatientAdverseEventExtract>();
        private ActionRegisterRepository _actionRegisterRepository;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiCentralContext();
            _patients=new List<PatientExtract>();
            _AdverseEventProfiles=new List<PatientAdverseEventProfile>();

            #region Test data
            //  facility
            _facilities = TestHelpers.GetTestData<Facility>(2).ToList();
            _facilities[0].Code = 100;
            _facilities[1].Code = 200;
            TestHelpers.CreateTestData(_context, _facilities);
            
            //  patient
            foreach (var facility in _facilities)
            {
                var patients = TestHelpers.GetTestPatientAdverseEventData(facility, 2, 2).ToList();
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

            #region Test AdverseEvent profiles

            var ids = _facilities.Select(x => x.Id).ToList();
            var facilities = _context.Facilities
                .Where(x => ids.Contains(x.Id))
                .Include(d => d.PatientExtracts.Select(v => v.PatientAdverseEventExtracts));

            foreach (var facility in facilities)
            {
                foreach (var patientExtract in facility.PatientExtracts)
                {
                    var v = new PatientAdverseEventProfile();
                    v.PatientInfo = patientExtract;
                    v.FacilityInfo = facility;
                    v.AdverseEventExtracts =
                        new PatientAdverseEventExtractDTO().GeneratePatientAdverseEventExtractDtOs(
                            patientExtract.PatientAdverseEventExtracts).ToList();
                    _AdverseEventProfiles.Add(v);
                }
            }


            #endregion

            #region NEW Test data
            //  NEW facility
            _newFacility = TestHelpers.GetTestData<Facility>(1).ToList().First();
            _newFacility.Name = "Migori County Hospital";
            _newFacility.Code = 300;
            _newFacility.Id=new Guid("73F27F28-B38C-4FE3-9234-A865008C9717");

            //  NEW patients
            _newFacilityPatients = TestHelpers.GetTestPatientAdverseEventData(_newFacility, 2, 1).ToList();
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

            #region NEW Test Data AdverseEvent profiles
            foreach (var patientExtract in _newFacility.PatientExtracts)
            {
                var v = new PatientAdverseEventProfile();
                v.PatientInfo = patientExtract;
                v.FacilityInfo = _newFacility;
                v.AdverseEventExtracts =
                    new PatientAdverseEventExtractDTO().GeneratePatientAdverseEventExtractDtOs(
                        patientExtract.PatientAdverseEventExtracts).ToList();
                _newAdverseEventProfiles.Add(v);
            }
            #endregion

            // UPDATE AdverseEvents for exisiting patient
        
            #region Updated AdverseEvents
            _updatedAdverseEventProfiles = new List<PatientAdverseEventProfile>();
            _updatedAdverseEventProfiles.AddRange(_AdverseEventProfiles);
            _newAdverseEventExtracts = TestHelpers.GetTestData<PatientAdverseEventExtract>(2).ToList();
            foreach (var extract in _newAdverseEventExtracts)
            {
                extract.PatientId = _patients[0].Id;
            }
            #endregion

            _facilityRepository = new FacilityRepository(_context);
            _patientExtractRepository = new PatientExtractRepository(_context);
            _patientAdverseEventRepository = new PatientAdverseEventRepository(_context);
            _actionRegisterRepository = new ActionRegisterRepository(_context);

        }

        [Test]
        public void should_Clear_AdverseEvents()
        {
            var patientExtract = _patients.First();

            var AdverseEvents = _context.PatientAdverseEventExtracts.Where(x => x.PatientId.Equals(patientExtract.Id)).ToList();
            Assert.True(AdverseEvents.Count>0);
            
            _patientAdverseEventRepository.ClearNew(patientExtract.Id);
            _patientAdverseEventRepository=new PatientAdverseEventRepository(_context);

            AdverseEvents = _context.PatientAdverseEventExtracts.Where(x => x.PatientId.Equals(patientExtract.Id)).ToList();
            Assert.True(AdverseEvents.Count == 0);
        }
        [Test]
        public void should_Sync_With_Updated_Patients_Added()
        {
            var facility = _facilities[0];
            int pid = 1;
            var newPatients = TestHelpers.GetTestPatientAdverseEventData(facility, 2, 3).ToList();
            foreach (var patientExtract in newPatients)
            {
                patientExtract.PatientCccNumber =$"10000/18/{pid}";
                facility.PatientExtracts.Add(patientExtract);
                pid++;
            }
            foreach (var patientExtract in facility.PatientExtracts)
            {
                var v = new PatientAdverseEventProfile();
                v.PatientInfo = patientExtract;
                v.FacilityInfo = _facilities[0];
                v.AdverseEventExtracts =
                    new PatientAdverseEventExtractDTO().GeneratePatientAdverseEventExtractDtOs(
                        patientExtract.PatientAdverseEventExtracts).ToList();
                _AdverseEventProfiles.Add(v);
            }
            
            _patientAdverseEventRepository.SyncNewPatients(_AdverseEventProfiles, _facilityRepository, new List<Guid>(), _actionRegisterRepository);
            _context = new DwapiCentralContext();
            var facilty = _context.Facilities.Where(x => x.Id == facility.Id)
                .Include(p => p.PatientExtracts.Select(v => v.PatientAdverseEventExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First(x=>x.Id==newPatients[0].Id);
            Assert.NotNull(patient);
            var AdverseEventis = patient.PatientAdverseEventExtracts.Count;
            Assert.IsTrue(AdverseEventis == 3);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| AdverseEvents:{AdverseEventis}");
        }
        [Test]
        public void should_Sync_With_Updated_Patients_Updated()
        {
            var patientInfo = _AdverseEventProfiles.Last().PatientInfo;
            patientInfo.MaritalStatus = "Married";
            patientInfo.PatientCccNumber = "15701-0001";

            _patientAdverseEventRepository.SyncNewPatients(_AdverseEventProfiles, _facilityRepository, new List<Guid>(), _actionRegisterRepository);
            _context = new DwapiCentralContext();
            var facilty = _context.Facilities.Where(x => x.Id == patientInfo.FacilityId)
                .Include(p => p.PatientExtracts.Select(v => v.PatientAdverseEventExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First(x => x.Id == patientInfo.Id);
            Assert.NotNull(patient);
            Assert.AreEqual("Married", patient.MaritalStatus);
            Assert.AreEqual("15701-0001", patient.PatientCccNumber);
            var AdverseEventis = patient.PatientAdverseEventExtracts.Count;
            Assert.IsTrue(AdverseEventis >0);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| AdverseEvents:{AdverseEventis}");
        }
        [Test]
        public void should_Sync_New_Facilty_With_Patients()
        {
            _patientAdverseEventRepository.SyncNewPatients(_newAdverseEventProfiles, _facilityRepository, new List<Guid>(), _actionRegisterRepository);
            _context = new DwapiCentralContext();
            var facilty = _context.Facilities.Where(x => x.Code == _newFacility.Code)
                .Include(p => p.PatientExtracts.Select(v => v.PatientAdverseEventExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First();
            Assert.NotNull(patient);
            var AdverseEventis = patient.PatientAdverseEventExtracts.Count;
            Assert.IsTrue(AdverseEventis == 1);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| AdverseEvents:{AdverseEventis}");
        }
        [Test]
        public void should_Sync_Patient_With_Updated_AdverseEvents_Added()
        {
            _updatedAdverseEventProfiles
                .First(x => x.PatientInfo.Id == _patients[0].Id)
                .AdverseEventExtracts
                .AddRange(new PatientAdverseEventExtractDTO().GeneratePatientAdverseEventExtractDtOs(_newAdverseEventExtracts).ToList());
            _patientAdverseEventRepository.SyncNewPatients(_updatedAdverseEventProfiles, _facilityRepository, new List<Guid>(), _actionRegisterRepository);
            _context = new DwapiCentralContext();

            var facilty = _context.Facilities.Where(x => x.Code == 100)
                .Include(p => p.PatientExtracts.Select(v => v.PatientAdverseEventExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            var patient = facilty.PatientExtracts.First(x => x.Id == _patients[0].Id);
            Assert.NotNull(patient);
            var AdverseEvents = patient.PatientAdverseEventExtracts.Count;
            Assert.IsTrue(AdverseEvents == 4);
            Console.WriteLine($"Faciltiy:{facilty}| Patient:{patient.PatientCccNumber}| AdverseEvents:{AdverseEvents}");
        }

        [Test]
        public void should_Sync_Patient_With_Updated_AdverseEvents_Modified()
        {
            var AdverseEvent = _AdverseEventProfiles.Last().AdverseEventExtracts.First();

            _AdverseEventProfiles.Last().AdverseEventExtracts.First().Severity = "MAUN";
            _patientAdverseEventRepository.SyncNewPatients(_AdverseEventProfiles, _facilityRepository, new List<Guid>(), _actionRegisterRepository);
            _context = new DwapiCentralContext();
            var patientExtract = _context.PatientExtracts.Where(x => x.Id == AdverseEvent.PatientId)
                .Include(v => v.PatientAdverseEventExtracts).FirstOrDefault();
            Assert.NotNull(patientExtract);

            var AdverseEvents = patientExtract.PatientAdverseEventExtracts.FirstOrDefault(x =>
                x.PatientId == AdverseEvent.PatientId && x.AdverseEvent == AdverseEvent.AdverseEvent);
            Assert.NotNull(AdverseEvents);
            Assert.AreEqual("MAUN", AdverseEvents.Severity);
        }

        [Test]
        public void should_Sync_Patient_With_Updated_AdverseEvents_Deleted()
        {
            var AdverseEvent = _AdverseEventProfiles.Last().AdverseEventExtracts.First();

            _AdverseEventProfiles.Last().AdverseEventExtracts.Remove(AdverseEvent);
            _patientAdverseEventRepository. SyncNewPatients(_AdverseEventProfiles, _facilityRepository, new List<Guid>(), _actionRegisterRepository);
            _context = new DwapiCentralContext();
            var patientExtract = _context.PatientExtracts.Where(x => x.Id == AdverseEvent.PatientId).Include(v => v.PatientAdverseEventExtracts).FirstOrDefault();
            Assert.NotNull(patientExtract);

            var AdverseEvents = patientExtract.PatientAdverseEventExtracts.FirstOrDefault(x => x.PatientId == AdverseEvent.PatientId && x.AdverseEvent == AdverseEvent.AdverseEvent);
            Assert.Null(AdverseEvents);
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
