using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientUploader.Core.Interfaces;

namespace PalladiumDwh.ClientUploader.Core.Services
{
    public class PushProfileService : IPushProfileService
    {

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        private readonly string _baseUrl;
        private readonly IClientPatientRepository _repository;
        private HttpClient _client;

        public PushProfileService(string baseUrl, IClientPatientRepository repository)
        {
            _baseUrl = baseUrl.EndsWith(@"/") ? baseUrl : $"{baseUrl}/";
            _repository = repository;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_baseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<PushResponse> PushAsync(IClientExtractProfile profile)
        {
            var response = await _client.PostAsJsonAsync(profile.EndPoint, profile);

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            
            var respone=new PushResponse(profile, content, true);
            UpdateExtract(respone,profile.Source);
            return respone;
        }

        //TODO make  UpdateExtract async
        private void UpdateExtract(PushResponse response,string source)
        {
            if (response.IsSuccess)
                _repository.UpdateProcessd(new ClientPatientExtract(){PatientPK = response.PatientPK,SiteCode = response.SiteCode},source);
        }

    }
}