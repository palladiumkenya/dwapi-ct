using System;
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
                p.Id = Guid.NewGuid();
                p.Processed = false;
                p.AddPatientArtExtracts(Builder<ClientPatientArtExtract>.CreateListOfSize(1).All().With(x=>x.Processed=false).Build().ToList());
                p.AddPatientBaselinesExtracts(Builder<ClientPatientBaselinesExtract>.CreateListOfSize(1).All().With(x => x.Processed = false).Build().ToList());
                p.AddPatientLaboratoryExtracts(Builder<ClientPatientLaboratoryExtract>.CreateListOfSize(count).All().With(x => x.Processed = false).Build().ToList());
                p.AddPatientPharmacyExtracts(Builder<ClientPatientPharmacyExtract>.CreateListOfSize(count).All().With(x => x.Processed = false).Build().ToList());
                p.AddPatientStatusExtracts(Builder<ClientPatientStatusExtract>.CreateListOfSize(1).All().With(x => x.Processed = false).Build().ToList());
                p.AddPatientVisitExtracts(Builder<ClientPatientVisitExtract>.CreateListOfSize(count).All().With(x => x.Processed = false).Build().ToList());
            }
            return patients;
        }
        public static IEnumerable<ClientPatientExtract> GetTestPatientWithExtracts(int patientCount, int count,int patientId, int siteCode)
        {
            var patients = Builder<ClientPatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.Id = Guid.NewGuid();
                p.PatientPK = patientId;
                p.SiteCode = siteCode;
                p.Processed = false;
                p.AddPatientArtExtracts(Builder<ClientPatientArtExtract>.CreateListOfSize(1).All().With(x => x.Processed = false).Build().ToList());
                p.AddPatientBaselinesExtracts(Builder<ClientPatientBaselinesExtract>.CreateListOfSize(1).All().With(x => x.Processed = false).Build().ToList());
                p.AddPatientLaboratoryExtracts(Builder<ClientPatientLaboratoryExtract>.CreateListOfSize(count).All().With(x => x.Processed = false).Build().ToList());
                p.AddPatientPharmacyExtracts(Builder<ClientPatientPharmacyExtract>.CreateListOfSize(count).All().With(x => x.Processed = false).Build().ToList());
                p.AddPatientStatusExtracts(Builder<ClientPatientStatusExtract>.CreateListOfSize(1).All().With(x => x.Processed = false).Build().ToList());
                p.AddPatientVisitExtracts(Builder<ClientPatientVisitExtract>.CreateListOfSize(count).All().With(x => x.Processed = false).Build().ToList());
            }
            return patients;
        }
    }
}