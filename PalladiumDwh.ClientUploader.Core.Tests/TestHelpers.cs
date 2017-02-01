using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientUploader.Core.Tests
{
    public static class TestHelpers
    {
        public static IEnumerable<ClientPatientExtract> GetTestPatientWithExtracts(int patientCount, int count)
        {
            var patients = Builder<ClientPatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.AddPatientArtExtracts(Builder<ClientPatientArtExtract>.CreateListOfSize(count).Build().ToList());
                p.AddPatientBaselinesExtracts(Builder<ClientPatientBaselinesExtract>.CreateListOfSize(count).Build().ToList());
                p.AddPatientLaboratoryExtracts(Builder<ClientPatientLaboratoryExtract>.CreateListOfSize(count).Build().ToList());
                p.AddPatientPharmacyExtracts(Builder<ClientPatientPharmacyExtract>.CreateListOfSize(count).Build().ToList());
                p.AddPatientStatusExtracts(Builder<ClientPatientStatusExtract>.CreateListOfSize(count).Build().ToList());
                p.AddPatientVisitExtracts(Builder<ClientPatientVisitExtract>.CreateListOfSize(count).Build().ToList());
            }
            return patients;
        }
    }
}