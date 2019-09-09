using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PalladiumDwh.Core.Exchange;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Services
{
   public class LiveSyncService : ILiveSyncService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings _serializerSettings;
      
        public LiveSyncService(string baseUrl)
        {
            Uri endPointA = new Uri(baseUrl); // this is the endpoint HttpClient will hit
            _httpClient = new HttpClient()
            {
                BaseAddress = endPointA,
            };
            _serializerSettings = new JsonSerializerSettings();
            _serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }


        public async Task<Result> SyncManifest(ManifestDto dto)
        {
            string requestEndpoint = "manifest";

            try
            {
                var content = JsonConvert.SerializeObject(dto, _serializerSettings);
                var toSend = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(requestEndpoint, toSend
                );
                response.EnsureSuccessStatusCode();
                return Result.Ok();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return Result.Fail(e.Message);
            }
        }

        public async Task<Result> SyncStats(IFacilityRepository facilityRepository, List<Guid> facilityId)
        {
            string requestEndpoint = "stats";
            var results=new List<Result>();

            var stats = facilityRepository.GetFacStats(facilityId);
            foreach (var stat in stats)
            {
                try
                {
                    var content = JsonConvert.SerializeObject(stat, _serializerSettings);
                    var toSend = new StringContent(content, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync(requestEndpoint, toSend);
                    response.EnsureSuccessStatusCode();
                    results.Add(Result.Ok());
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    results.Add(Result.Fail(e.Message));
                }
            }
            if(results.Any(x=>x.IsFailure))
                return Result.Fail(results.First(x=>x.IsFailure).Error);

            return Result.Ok();
        }
    }
}
