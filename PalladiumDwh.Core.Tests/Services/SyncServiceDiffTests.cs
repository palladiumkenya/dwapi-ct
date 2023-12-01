using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Services;
using PalladiumDwh.Infrastructure.Data;
using PalladiumDwh.Infrastructure.Data.Repository;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Tests.Services
{
    [TestFixture]
    public class SyncServiceDiffTests
    {
        private ISyncService _syncService;
        private DwapiCentralContext _context;
        private List<PatientExtract> _thePatients;
        private PatientExtract _thePatientFirstVisit, _thePatientNextVisit;
        private Facility _facility;
        

        [SetUp]
        public void SetUp()
        {
            _context = TestInitializer.Container.GetInstance<DwapiCentralContext>();
            _syncService = TestInitializer.Container.GetInstance<ISyncService>();
            _facility = _syncService.GetFacility(900000);
            _thePatients = TestHelpers.GetTestPatientWithExtracts(_facility, 2, 10).ToList();
            _thePatientFirstVisit= TestHelpers.GetTestPatientWithExtracts(_facility, 1, 1).ToList().First();
            _thePatientNextVisit = TestHelpers.GetTestPatientWithExtracts(_facility, 1, 1).ToList().First();
        }

        [TestCase("PatientVisitProfile")]
        public void should_Sync_New(string queue)
        {
            var repository= TestInitializer.Container.GetInstance<IPatientExtractRepository>();
            var profiles = new List<PatientVisitProfile>();
            foreach(var patient in _thePatients)
            {
                profiles.Add(PatientVisitProfile.Create(_facility, patient));
            }

            var manifest = GenerateManifest(_facility, _thePatients);

            _syncService.SyncManifest(manifest);
            _syncService.SyncVisitNew(profiles);
            _syncService.Commit(queue);

            var savedPatient = repository.Find(profiles.First().PatientInfo.Id);
            Assert.IsNotNull(savedPatient);
            Assert.IsTrue(savedPatient.PatientVisitExtracts.Count > 0);
        }
        

        [TestCase("PatientVisitProfile")]
        public void should_Sync_Upadtes(string queue)
        {
            var visitDate = DateTime.Now.Date.AddDays(-2);
            var repository = TestInitializer.Container.GetInstance<IPatientExtractRepository>();

            #region Visit 0
            
            var profiles = new List<PatientVisitProfile>();
            var patient = _thePatientFirstVisit;
            patient.PatientPID = 100;
            patient.PatientCccNumber = "9000000100";

            patient.PatientVisitExtracts.ToList().ForEach(x =>
            {
                x.VisitId = 1000;
                x.VisitDate = visitDate;
                x.Height = 164;
                x.Weight = 60.1m;
                x.Date_Created = visitDate;
                x.Date_Last_Modified = visitDate;
            });

            profiles.Add(PatientVisitProfile.Create(_facility, patient));
            var manifest = GenerateManifest(_facility, new List<PatientExtract>() { patient });

            _syncService.SyncManifest(manifest);
            _syncService.SyncVisitNew(profiles);
            _syncService.Commit(queue);

            var savedPatient = repository.Find(profiles.First().PatientInfo.Id);
            Assert.IsNotNull(savedPatient);
            Assert.AreEqual(164, savedPatient.PatientVisitExtracts.First().Height);
            Assert.AreEqual(60.1m, savedPatient.PatientVisitExtracts.First().Weight);

            #endregion

            #region Visit 1

            var profilesNext = new List<PatientVisitProfile>();
            var patientNext = _thePatientNextVisit;
            patientNext.PatientPID = 100;
            patientNext.PatientCccNumber = "9000000100";

            patientNext.PatientVisitExtracts.ToList().ForEach(x =>
            {
                x.VisitId = 1000;
                x.VisitDate = visitDate;
                x.Height = 164;
                x.Weight = 66;
                x.Date_Created = visitDate;
                x.Date_Last_Modified = DateTime.Now;
            });

            profilesNext.Add(PatientVisitProfile.Create(_facility, patientNext));

            var manifest2 = GenerateManifest(_facility, new List<PatientExtract>() { patientNext });

            var _syncService2= TestInitializer.Container.GetInstance<ISyncService>();
            _syncService2.SyncManifest(manifest2);
            _syncService2.SyncVisitNew(profilesNext);
            _syncService2.Commit(queue);
            var repository2 = TestInitializer.Container.GetInstance<IPatientExtractRepository>();
            var updatedPatient = repository2.Find(profiles.First().PatientInfo.Id);
            Assert.IsNotNull(updatedPatient);
            Assert.AreEqual(164, updatedPatient.PatientVisitExtracts.First().Height);
            Assert.AreEqual(66, updatedPatient.PatientVisitExtracts.First().Weight);

            #endregion
        }

        private Manifest GenerateManifest(Facility facility, List<PatientExtract> patients)
        {
            var manifest = new Manifest(facility.Code);
            foreach (var p in patients)
            {
                manifest.AddPatientPk(p.PatientPID);
            }
            return manifest;
        }
    }
}
