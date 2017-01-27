using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Services;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.ClientReader.Core.Tests.Model
{
    [TestFixture]
    public class ReadExtractsTests
    {
        private IReadExtractService _readExtractsService;
        private List<PatientExtractRow> _patientExtractRows;


        [SetUp]
        public void Setup()
        {
            _patientExtractRows = Builder<PatientExtractRow>.CreateListOfSize(5).Build().ToList();
            int code = 1000;
            foreach (var f in _patientExtractRows)
            {
                f.SiteCode = code;
                f.FacilityName = "Test";
                f.Project = "P";
                f.Emr = "X";
                code++;
            }
            foreach (var f in _patientExtractRows.Take(4))
            {
                f.SiteCode = 3000;
                f.FacilityName = "Test 3";
                f.Project = "P";
                f.Emr = "X";
            }

            var patient = Builder<PatientExtract>.CreateNew().Build();
            foreach (var e in _patientExtractRows)
            {
                e.PatientPK = patient.PatientPID;
            }
        }

        [Test]
        public void should_GetFacilityData()
        {
            _readExtractsService = new ReadExtractsService();
            var facilities = _readExtractsService.GetFacilityData(_patientExtractRows).ToList();

            Assert.IsTrue(facilities.Count == 2);


            var noFacilities = _readExtractsService.GetFacilityData(null).ToList();
            Assert.IsTrue(noFacilities.Count == 0);
            var noFacilities2 = _readExtractsService.GetFacilityData(new List<PatientExtractRow>()).ToList();
            Assert.IsTrue(noFacilities2.Count == 0);
        }


        [Test]
        public void should_GetPatientData()
        {
            _readExtractsService = new ReadExtractsService();
            var facilities = _readExtractsService.GetFacilityData(_patientExtractRows).ToList();

            var patients = _readExtractsService.GetPatientData(facilities, _patientExtractRows).ToList();
            Assert.IsTrue(patients.Count == 5);

            facilities.First().Code = -1;
            var patients2 = _readExtractsService.GetPatientData(facilities, _patientExtractRows).ToList();
            Assert.IsTrue(patients2.Count < 5);

            var patients3 = _readExtractsService.GetPatientData(null, _patientExtractRows).ToList();
            Assert.IsTrue(patients3.Count == 0);
            var patients4 = _readExtractsService.GetPatientData(facilities, null).ToList();
            Assert.IsTrue(patients4.Count == 0);
        }

        [Test]
        public void should_GetPatientBaselineData()
        {
           
            _readExtractsService = new ReadExtractsService();
            var patient = Builder<PatientExtract>.CreateNew().Build();
            var extracts = TestHelpers.GetTestPatientRowExtracts<PatientBaselinesExtractRow>(patient, 5).ToList();


            var patients = _readExtractsService.GetPatientBaselineData(patient,extracts).ToList();
            Assert.IsTrue(patients.Count == 5);

  
            var patients3 = _readExtractsService.GetPatientBaselineData(null, extracts).ToList();
            Assert.IsTrue(patients3.Count == 0);
            var patients4 = _readExtractsService.GetPatientBaselineData(patient, null).ToList();
            Assert.IsTrue(patients4.Count == 0);
        }

        [Test]
        public void should_GetPatientArtData()
        {
            _readExtractsService = new ReadExtractsService();
            var patient = Builder<PatientExtract>.CreateNew().Build();
            var extracts = TestHelpers.GetTestPatientRowExtracts<PatientArtExtractRow>(patient, 5).ToList();


            var patients = _readExtractsService.GetPatientArtData(patient, extracts).ToList();
            Assert.IsTrue(patients.Count == 5);


            var patients3 = _readExtractsService.GetPatientArtData(null, extracts).ToList();
            Assert.IsTrue(patients3.Count == 0);
            var patients4 = _readExtractsService.GetPatientArtData(patient, null).ToList();
            Assert.IsTrue(patients4.Count == 0);
        }

        [Test]
        public void should_GetPatientLabData()
        {
            _readExtractsService = new ReadExtractsService();
            var patient = Builder<PatientExtract>.CreateNew().Build();
            var extracts = TestHelpers.GetTestPatientRowExtracts<PatientLaboratoryExtractRow>(patient, 5).ToList();


            var patients = _readExtractsService.GetPatientLabData(patient, extracts).ToList();
            Assert.IsTrue(patients.Count == 5);


            var patients3 = _readExtractsService.GetPatientLabData(null, extracts).ToList();
            Assert.IsTrue(patients3.Count == 0);
            var patients4 = _readExtractsService.GetPatientLabData(patient, null).ToList();
            Assert.IsTrue(patients4.Count == 0);
        }

        [Test]
        public void should_GetPatientPharmData()
        {
            _readExtractsService = new ReadExtractsService();
            var patient = Builder<PatientExtract>.CreateNew().Build();
            var extracts = TestHelpers.GetTestPatientRowExtracts<PatientPharmacyExtractRow>(patient, 5).ToList();


            var patients = _readExtractsService.GetPatientPharmData(patient, extracts).ToList();
            Assert.IsTrue(patients.Count == 5);


            var patients3 = _readExtractsService.GetPatientPharmData(null, extracts).ToList();
            Assert.IsTrue(patients3.Count == 0);
            var patients4 = _readExtractsService.GetPatientPharmData(patient, null).ToList();
            Assert.IsTrue(patients4.Count == 0);
        }

        [Test]
        public void should_GetPatientStatusData()
        {
            _readExtractsService = new ReadExtractsService();
            var patient = Builder<PatientExtract>.CreateNew().Build();
            var extracts = TestHelpers.GetTestPatientRowExtracts<PatientStatusExtractRow>(patient, 5).ToList();

            var patients = _readExtractsService.GetPatientStatusData(patient, extracts).ToList();
            Assert.IsTrue(patients.Count == 5);

            var patients3 = _readExtractsService.GetPatientStatusData(null, extracts).ToList();
            Assert.IsTrue(patients3.Count == 0);
            var patients4 = _readExtractsService.GetPatientStatusData(patient, null).ToList();
            Assert.IsTrue(patients4.Count == 0);
        }

        [Test]
        public void should_GetPatientVisitData()
        {
            _readExtractsService = new ReadExtractsService();
            var patient = Builder<PatientExtract>.CreateNew().Build();
            var extracts = TestHelpers.GetTestPatientRowExtracts<PatientVisitExtractRow>(patient, 5).ToList();


            var patients = _readExtractsService.GetPatientVisitData(patient, extracts).ToList();
            Assert.IsTrue(patients.Count == 5);


            var patients3 = _readExtractsService.GetPatientVisitData(null, extracts).ToList();
            Assert.IsTrue(patients3.Count == 0);
            var patients4 = _readExtractsService.GetPatientVisitData(patient, null).ToList();
            Assert.IsTrue(patients4.Count == 0);
        }
    }
}
