using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FizzWare.NBuilder;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Tests
{
    public static class TestHelpers
    {
        public static void CreateTestData<T>(DbContext context, IEnumerable<T> entities) where T : Entity
        {
            context.Set<T>().AddRange(entities);
            context.SaveChanges();
        }
        public static IEnumerable<T> GetTestData<T>(int count) where T : Entity
        {
            return Builder<T>.CreateListOfSize(count).Build();
        }
        public static IEnumerable<PatientExtract> GetTestPatientArtData(Facility facility, int patientCount, int visitCount)
        {
            var patients = Builder<PatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.FacilityId = facility.Id;
                var extracts = Builder<PatientArtExtract>.CreateListOfSize(visitCount).Build().ToList();
                p.AddPatientArtExtracts(extracts);
            }
            return patients;
        }
        public static IEnumerable<PatientExtract> GetTestPatientVisitsData(Facility facility,int patientCount,int visitCount)
        {
            var patients=Builder<PatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.FacilityId=facility.Id;
                var extracts = Builder<PatientVisitExtract>.CreateListOfSize(visitCount).Build().ToList();
                p.AddPatientVisitExtracts(extracts);
            }
            return patients;
        }
    }
}