using System;
using System.Net.Http;
using System.Net.Http.Extensions.Compression.Client;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using PalladiumDwh.ClientReader.Core.Interfaces.Profiles;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientUploader.Core.Interfaces;
using PalladiumDwh.ClientUploader.Core.Model;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientUploader.Core.Services
{
    public class PushProfileService : IPushProfileService
    {

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly int _maxRetries = 3;

        private readonly string _baseUrl;
        private readonly IClientPatientRepository _repository;
        private HttpClient _client;

        private IProgress<DProgress> _progress;
        private string _progressStatus;

        public PushProfileService(string baseUrl, IClientPatientRepository repository)
        {
            _baseUrl = baseUrl.EndsWith(@"/") ? baseUrl : $"{baseUrl}/";
            _repository = repository;
            _client = new HttpClient(new ClientCompressionHandler(4096,new GZipCompressor(), new DeflateCompressor()));
            _client.BaseAddress = new Uri(_baseUrl);
            _client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            _client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> SpotAsync(Manifest manifest, IProgress<DProgress> progress = null)
        {
            Log.Debug("spotting...");

            HttpResponseMessage response = null;
            string spotResponse = string.Empty;

            try
            {
                bool postSuccess = false;
                int retryCount = 0;

                progress?.ReportStatus("checking Facility details...");

                while (postSuccess == false && retryCount <= _maxRetries)
                {
                    response = await _client.PostAsJsonAsync("Spot", manifest);
                    postSuccess = response.IsSuccessStatusCode;
                    if (!postSuccess)
                    {
                        progress?.ReportStatus($"re-checking Facility details Attempt {retryCount}...");
                        retryCount++;
                    }
                }

                spotResponse = await response.Content.ReadAsStringAsync();
                if (postSuccess)
                {
                    Log.Debug(spotResponse);
                    progress?.ReportStatus($"Facility:{spotResponse}");
                }
                else
                {
                    throw new Exception(spotResponse);
                }

            }
            catch (Exception e)
            {
                progress?.ReportStatus("Error ocurred !");
                Log.Debug(e);
                throw;
            }
            return spotResponse;
        }

        public async Task<PushResponse> PushAsync(IClientExtractProfile profile, bool processResponse = true)
        {
            HttpResponseMessage response = null;
            string content = string.Empty;
            PushResponse pushResponse = null;

            try
            {
                
                    response = await _client.PostAsJsonAsync(profile.EndPoint, profile);
                
            }
            catch (Exception e)
            {
                //  network error
                Log.Debug(e);
                pushResponse = new PushResponse(profile, content, $"Network Error:{e.Message}", false);
            }
            
            if (null != response)
            {
                try
                {
                    content = await response.Content.ReadAsStringAsync();
                    pushResponse = response.IsSuccessStatusCode ? new PushResponse(profile, content, "Sent", true) : new PushResponse(profile, content, $"Send Error:{content}", false);
                }
                catch (Exception e)
                {
                    // send error
                    Log.Debug(e);
                    pushResponse = new PushResponse(profile, content, $"Send Error:{e.Message}", false);
                }
            }

            if(processResponse)
                UpdateExtract(pushResponse,profile.Source);

            return pushResponse;
        }

        public Task<PushResponse> PushAsync(SiteProfile siteProfile)
        {
            throw new NotImplementedException();
        }

        //TODO make  UpdateExtract async
        private void UpdateExtract(PushResponse response, string source)
        {
            //if (response.IsSuccess)
            _repository.UpdatePush(
                new ClientPatientExtract() {PatientPK = response.PatientPK, SiteCode = response.SiteCode}, source,
                response);
        }


     
    }
}