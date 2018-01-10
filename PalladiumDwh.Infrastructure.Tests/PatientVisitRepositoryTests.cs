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

        private FacilityRepository _facilityRepository;
        private PatientVisitRepository _patientVisitRepository;
        private Facility _newFacility;
        private List<PatientExtract> _newFacilityPatients;
        private List<PatientVisitExtract> _newVisitExtracts = new List<PatientVisitExtract>();

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiCentralContext();

            //  facility
            _facilities = TestHelpers.GetTestData<Facility>(2).ToList();
            _facilities[0].Code = 100;
            _facilities[1].Code = 200;
            TestHelpers.CreateTestData(_context, _facilities);

            //  NEW facility
            _newFacility = TestHelpers.GetTestData<Facility>(1).ToList().First();
            _newFacility.Name = "Migori County Hospital";
            _newFacility.Code = 300;

            //  patient
            foreach (var facility in _facilities)
            {
               var patients = TestHelpers.GetTestPatientVisitsData(facility,2, 2).ToList();
                _patients.AddRange(patients);
            }
            int pid = 1000;
            foreach (var p in _patients)
            {
                p.PatientPID = pid;
                pid++;
            }
            TestHelpers.CreateTestData(_context, _patients);

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
            // New visit for exisiting patient
            _newVisitExtracts = TestHelpers.GetTestData<PatientVisitExtract>(2).ToList();
            _newFacility.PatientExtracts.ToList()[0].AddPatientVisitExtracts(_newVisitExtracts);
            

            var existingPatient = TestHelpers.GetTestPatientVisitsData(_facilities[0], 1, 2).ToList().First();
            foreach (var v in existingPatient.PatientVisitExtracts.ToList())
            {
                v.PatientId = _facilities[0].PatientExtracts.First().Id;
                _facilities[0].PatientExtracts.ToList()[0].PatientVisitExtracts.Add(v);
            }


            _facilityRepository = new FacilityRepository(_context);
            _patientExtractRepository = new PatientExtractRepository(_context);
            _patientVisitRepository = new PatientVisitRepository(_context);


            var facilities = _context.Facilities.Include(d => d.PatientExtracts.Select(v=>v.PatientVisitExtracts));

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
            



            //TestHelpers.AddNew(_visitProfiles, 5);
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
        public void should_Sync_New_Facilty_With_Patients()
        {
            _patientVisitRepository.SyncNewPatients(_newVisitProfiles, _facilityRepository);
            _context = new DwapiCentralContext();
            var facilty = _context.Facilities.Where(x=>x.Code==_newFacility.Code).Include(p=>p.PatientExtracts.Select(v=>v.PatientVisitExtracts)).FirstOrDefault();
            Assert.NotNull(facilty);
            Assert.IsTrue(facilty.PatientExtracts.First().PatientVisitExtracts.Count>0); 
        }

        [Test]
        public void should_Sync_Update_Patients()
        {
            var manifest = new Manifest(_newFacility.Code);
            var currentPatients = _patients.Where(x => x.PatientPID > 1000);
            foreach (var p in currentPatients)
            {
                manifest.AddPatientPk(p.PatientPID);
            }

            _patientExtractRepository = new PatientExtractRepository(_dbcontext);
            _patientExtractRepository.ClearManifest(manifest);

            _patientExtractRepository = new PatientExtractRepository(new DwapiCentralContext());

            var cleanPatients = _patientExtractRepository.GetAllBy(x => x.FacilityId == _newFacility.Id).ToList();

            Assert.IsTrue(cleanPatients.Count == 4);
        }

   

        [Test]
        public void should_Sync_Visits()
        {
            _patientExtractRepository = new PatientExtractRepository(_dbcontext);
            var id = _patientExtractRepository.GetPatientByIds(_newFacility.Id,1001);
            Assert.False(id.IsNullOrEmpty());
            Console.WriteLine($"Patien ID:{id}");
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
