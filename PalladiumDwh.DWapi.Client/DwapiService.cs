using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PalladiumDwh.DWapi.Client.Model;
using PalladiumDwh.DWapi.Client.Model.Profiles;
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
        public Facility Get(int id)
        {
            var request = new RestRequest($"PatientArt/{id}", Method.GET);
            request.JsonSerializer = new NewtonsoftJsonSerializer();
            request.RequestFormat = DataFormat.Json;
            request.OnBeforeDeserialization = restResponse => { restResponse.ContentType = "application/json"; };
            var response = _client.Execute(request);
            return JsonConvert.DeserializeObject<Facility>(response.Content);
        }

        public Guid? Post(PatientARTProfile profile)
        {
            var request = new RestRequest("PatientArt/", Method.POST);
            request.JsonSerializer = new NewtonsoftJsonSerializer();
            request.RequestFormat = DataFormat.Json;
            request.AddBody(profile);

            var res = _client.Execute(request);
            return new Guid(res.Content.ToString());
        }
    }
}
