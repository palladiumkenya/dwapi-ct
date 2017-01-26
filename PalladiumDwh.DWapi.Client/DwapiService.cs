using System.Net;
using PalladiumDwh.Shared.Model.Profile;
using RestSharp;
using RestSharp.Newtonsoft.Json;
using RestRequest = RestSharp.RestRequest;

namespace PalladiumDwh.DWapi.Client
{
    public class DwapiService : IDwapiService
    {
        private readonly RestClient _client;
        
        public DwapiService(string url)
        {
            _client = new RestClient(url);
        }

        public bool Post(PatientARTProfile profile)
        {
            var request = new RestRequest("PatientArt/", Method.POST);
            request.JsonSerializer = new NewtonsoftJsonSerializer();
            request.RequestFormat = DataFormat.Json;
            request.AddBody(profile);

            var res = _client.Execute(request);
            return (res.StatusCode == HttpStatusCode.OK);
        }
        public bool Post(PatientBaselineProfile profile)
        {
            var request = new RestRequest("PatientBaselines/", Method.POST);
            request.JsonSerializer = new NewtonsoftJsonSerializer();
            request.RequestFormat = DataFormat.Json;
            request.AddBody(profile);

            var res = _client.Execute(request);
            return (res.StatusCode == HttpStatusCode.OK);
        }
        public bool Post(PatientLabProfile profile)
        {
            var request = new RestRequest("PatientLabs/", Method.POST);
            request.JsonSerializer = new NewtonsoftJsonSerializer();
            request.RequestFormat = DataFormat.Json;
            request.AddBody(profile);

            var res = _client.Execute(request);
            return (res.StatusCode == HttpStatusCode.OK);
        }
        public bool Post(PatientPharmacyProfile profile)
        {
            var request = new RestRequest("PatientPharmacy/", Method.POST);
            request.JsonSerializer = new NewtonsoftJsonSerializer();
            request.RequestFormat = DataFormat.Json;
            request.AddBody(profile);

            var res = _client.Execute(request);
            return (res.StatusCode == HttpStatusCode.OK);
        }
        public bool Post(PatientStatusProfile profile)
        {
            var request = new RestRequest("PatientStatus/", Method.POST);
            request.JsonSerializer = new NewtonsoftJsonSerializer();
            request.RequestFormat = DataFormat.Json;
            request.AddBody(profile);

            var res = _client.Execute(request);
            return (res.StatusCode == HttpStatusCode.OK);
        }
        public bool Post(PatientVisitProfile profile)
        {
            var request = new RestRequest("PatientVisits/", Method.POST);
            request.JsonSerializer = new NewtonsoftJsonSerializer();
            request.RequestFormat = DataFormat.Json;
            request.AddBody(profile);

            var res = _client.Execute(request);
            return (res.StatusCode == HttpStatusCode.OK);
        }
    }
}
