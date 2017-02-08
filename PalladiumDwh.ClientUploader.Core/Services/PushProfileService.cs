using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientUploader.Core.Interfaces;

namespace PalladiumDwh.ClientUploader.Core.Services
{
    public class PushProfileService:IPushProfileService
    {
        private readonly string _baseUrl;
        private HttpClient _client;
        public PushProfileService(string baseUrl)
        {
            _baseUrl = baseUrl;
            _client=new HttpClient();
            _client.BaseAddress = new Uri(baseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> PushAsync(IClientExtractProfile profile)
        {
            var response= await _client.PostAsJsonAsync(profile.EndPoint, profile);

            return response; 
        }
    }
}