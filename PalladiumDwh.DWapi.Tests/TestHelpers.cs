using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using FizzWare.NBuilder;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.DWapi.Tests
{
    public static class TestHelpers
    {
        public static void SetupControllerForTests(ApiController controller, string url, string action)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", $"{action}" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        public static void CreateTestData<T>(DbContext context, IEnumerable<T> entities) where T : Entity
        {
            context.Set<T>().AddRange(entities);
            context.SaveChanges();
        }
        public static IEnumerable<T> GetTestData<T>(int count) where T : Entity
        {
            return Builder<T>.CreateListOfSize(count).Build();
        }

        public static IEnumerable<PatientExtract> GetTestPatientData(Facility facility, int patientCount)
        {
            var patients = Builder<PatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.FacilityId = facility.Id;
            }
            return patients;
        }


        public static IEnumerable<PatientExtract> GetTestPatientWithExtracts(Facility facility, int patientCount, int count)
        {
            var patients = Builder<PatientExtract>.CreateListOfSize(patientCount).Build().ToList();
            foreach (var p in patients)
            {
                p.FacilityId = facility.Id;
                p.AddPatientArtExtracts(Builder<PatientArtExtract>.CreateListOfSize(count).Build().ToList());
                p.AddPatientBaselinesExtracts(Builder<PatientBaselinesExtract>.CreateListOfSize(count).Build().ToList());
                p.AddPatientLaboratoryExtracts(Builder<PatientLaboratoryExtract>.CreateListOfSize(count).Build().ToList());
                p.AddPatientPharmacyExtracts(Builder<PatientPharmacyExtract>.CreateListOfSize(count).Build().ToList());
                p.AddPatientStatusExtracts(Builder<PatientStatusExtract>.CreateListOfSize(count).Build().ToList());
                p.AddPatientVisitExtracts(Builder<PatientVisitExtract>.CreateListOfSize(count).Build().ToList());
                p.AddPatientAdverseEventExtracts(Builder<PatientAdverseEventExtract>.CreateListOfSize(count).Build().ToList());
                p.AddCervicalCancerScreeningExtracts(Builder<CervicalCancerScreeningExtract>.CreateListOfSize(count).Build().ToList());
                p.AddIITRiskScoresExtracts(Builder<IITRiskScoresExtract>.CreateListOfSize(count).Build().ToList());


            }
            return patients;
        }
    }
}
