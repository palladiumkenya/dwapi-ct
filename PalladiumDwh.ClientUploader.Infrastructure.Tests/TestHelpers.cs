using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FizzWare.NBuilder;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientUploader.Infrastructure.Tests
{
    public static class TestHelpers
    {
        public static void CreateTestData<T>(DbContext context, IEnumerable<T> entities) where T : class 
        {
            context.Set<T>().AddRange(entities);
            context.SaveChanges();
        }
        public static IEnumerable<T> GetTestData<T>(int count) where T : class
        {
            return Builder<T>.CreateListOfSize(count).Build();
        }
        public static IEnumerable<ClientPatientExtract> GetTestPatientData(ClientFacility facility, int patientCount)
        {
            var patients = Builder<ClientPatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.SiteCode = facility.Code;
            }
            return patients;
        }

        public static IEnumerable<ClientPatientExtract> GetTestPatientWithExtracts(ClientFacility facility, int patientCount, int count)
        {
            var patients = Builder<ClientPatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {

                p.SiteCode = facility.Code;
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