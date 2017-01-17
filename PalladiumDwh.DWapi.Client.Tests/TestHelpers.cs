using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FizzWare.NBuilder;
using PalladiumDwh.DWapi.Client.Model;
using PalladiumDwh.Shared;

namespace PalladiumDwh.DWapi.Client.Tests
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
        public static IEnumerable<PatientExtract> GetTestPatientARTData(Facility facility,int patientCount,int visitCount)
        {
            var patients=Builder<PatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.FacilityId=facility.Id;
                var artExtracts = Builder<PatientArtExtract>.CreateListOfSize(visitCount).Build().ToList();
                p.AddPatientArtExtracts(artExtracts);
            }
            return patients;
        }
    }
}