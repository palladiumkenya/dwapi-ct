using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizzWare.NBuilder;
using NUnit.Framework.Internal;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.ClientReader.Core.Tests.Model
{
    [TestFixture]
    public class ReadExtractsTests
    {
        private IReadExtracts _readExtracts;
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
            _readExtracts = new ReadExtracts();
            var facilities = _readExtracts.GetFacilityData(_patientExtractRows).ToList();

            Assert.IsTrue(facilities.Count == 2);


            var noFacilities = _readExtracts.GetFacilityData(null).ToList();
            Assert.IsTrue(noFacilities.Count == 0);
            var noFacilities2 = _readExtracts.GetFacilityData(new List<PatientExtractRow>()).ToList();
            Assert.IsTrue(noFacilities2.Count == 0);
        }


        [Test]
        public void should_GetPatientData()
        {
            _readExtracts = new ReadExtracts();
            var facilities = _readExtracts.GetFacilityData(_patientExtractRows).ToList();

            var patients = _readExtracts.GetPatientData(facilities, _patientExtractRows).ToList();
            Assert.IsTrue(patients.Count == 5);

            facilities.First().Code = -1;
            var patients2 = _readExtracts.GetPatientData(facilities, _patientExtractRows).ToList();
            Assert.IsTrue(patients2.Count < 5);

            var patients3 = _readExtracts.GetPatientData(null, _patientExtractRows).ToList();
            Assert.IsTrue(patients3.Count == 0);
            var patients4 = _readExtracts.GetPatientData(facilities, null).ToList();
            Assert.IsTrue(patients4.Count == 0);
        }

        [Test]
        public void should_GetPatientBaselineData()
        {
           
            _readExtracts = new ReadExtracts();
            var patient = Builder<PatientExtract>.CreateNew().Build();
            var extracts = TestHelpers.GetTestPatientRowExtracts<PatientBaselinesExtractRow>(patient, 5).ToList();


            var patients = _readExtracts.GetPatientBaselineData(patient,extracts).ToList();
            Assert.IsTrue(patients.Count == 5);

  
            var patients3 = _readExtracts.GetPatientBaselineData(null, extracts).ToList();
            Assert.IsTrue(patients3.Count == 0);
            var patients4 = _readExtracts.GetPatientBaselineData(patient, null).ToList();
            Assert.IsTrue(patients4.Count == 0);
        }

        [Test]
        public void should_GetPatientArtData()
        {
            _readExtracts = new ReadExtracts();
            var patient = Builder<PatientExtract>.CreateNew().Build();
            var extracts = TestHelpers.GetTestPatientRowExtracts<PatientArtExtractRow>(patient, 5).ToList();


            var patients = _readExtracts.GetPatientArtData(patient, extracts).ToList();
            Assert.IsTrue(patients.Count == 5);


            var patients3 = _readExtracts.GetPatientArtData(null, extracts).ToList();
            Assert.IsTrue(patients3.Count == 0);
            var patients4 = _readExtracts.GetPatientArtData(patient, null).ToList();
            Assert.IsTrue(patients4.Count == 0);
        }

        [Test]
        public void should_GetPatientLabData()
        {
            _readExtracts = new ReadExtracts();
            var patient = Builder<PatientExtract>.CreateNew().Build();
            var extracts = TestHelpers.GetTestPatientRowExtracts<PatientLaboratoryExtractRow>(patient, 5).ToList();


            var patients = _readExtracts.GetPatientLabData(patient, extracts).ToList();
            Assert.IsTrue(patients.Count == 5);


            var patients3 = _readExtracts.GetPatientLabData(null, extracts).ToList();
            Assert.IsTrue(patients3.Count == 0);
            var patients4 = _readExtracts.GetPatientLabData(patient, null).ToList();
            Assert.IsTrue(patients4.Count == 0);
        }

        [Test]
        public void should_GetPatientPharmData()
        {
            _readExtracts = new ReadExtracts();
            var patient = Builder<PatientExtract>.CreateNew().Build();
            var extracts = TestHelpers.GetTestPatientRowExtracts<PatientPharmacyExtractRow>(patient, 5).ToList();


            var patients = _readExtracts.GetPatientPharmData(patient, extracts).ToList();
            Assert.IsTrue(patients.Count == 5);


            var patients3 = _readExtracts.GetPatientPharmData(null, extracts).ToList();
            Assert.IsTrue(patients3.Count == 0);
            var patients4 = _readExtracts.GetPatientPharmData(patient, null).ToList();
            Assert.IsTrue(patients4.Count == 0);
        }

        [Test]
        public void should_GetPatientStatusData()
        {
            _readExtracts = new ReadExtracts();
            var patient = Builder<PatientExtract>.CreateNew().Build();
            var extracts = TestHelpers.GetTestPatientRowExtracts<PatientStatusExtractRow>(patient, 5).ToList();

            var patients = _readExtracts.GetPatientStatusData(patient, extracts).ToList();
            Assert.IsTrue(patients.Count == 5);

            var patients3 = _readExtracts.GetPatientStatusData(null, extracts).ToList();
            Assert.IsTrue(patients3.Count == 0);
            var patients4 = _readExtracts.GetPatientStatusData(patient, null).ToList();
            Assert.IsTrue(patients4.Count == 0);
        }

        [Test]
        public void should_GetPatientVisitData()
        {
            _readExtracts = new ReadExtracts();
            var patient = Builder<PatientExtract>.CreateNew().Build();
            var extracts = TestHelpers.GetTestPatientRowExtracts<PatientVisitExtractRow>(patient, 5).ToList();


            var patients = _readExtracts.GetPatientVisitData(patient, extracts).ToList();
            Assert.IsTrue(patients.Count == 5);


            var patients3 = _readExtracts.GetPatientVisitData(null, extracts).ToList();
            Assert.IsTrue(patients3.Count == 0);
            var patients4 = _readExtracts.GetPatientVisitData(patient, null).ToList();
            Assert.IsTrue(patients4.Count == 0);
        }
    }
}
