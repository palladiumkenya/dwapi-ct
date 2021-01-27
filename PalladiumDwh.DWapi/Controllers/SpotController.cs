using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using log4net;
using PalladiumDwh.Core.Exchange;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.DWapi.Controllers
{
    public class SpotController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IPatientExtractRepository _patientExtractRepository;
        private readonly IMessagingSenderService _messagingService;
        private readonly string _gateway = typeof(Manifest).Name.ToLower();
        private readonly ILiveSyncService _liveSyncService;
        private readonly IFacilityRepository _facilityRepository;

        public SpotController(IMessagingSenderService messagingService,IPatientExtractRepository patientExtractRepository, ILiveSyncService liveSyncService, IFacilityRepository facilityRepository)
        {
            _messagingService = messagingService;
            _messagingService.Initialize(_gateway);
            _patientExtractRepository = patientExtractRepository;
            _liveSyncService = liveSyncService;
            _facilityRepository = facilityRepository;
        }

        public async Task<HttpResponseMessage> Post([FromBody] Manifest manifest)
        {
            MasterFacility masterFacility = null;

            if (null != manifest)
            {
                if (!manifest.IsValid())
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new HttpError($"Invalid Manifest,Please ensure the SiteCode [{manifest.SiteCode}] is valid and there exists at least one (1) Patient record"));
                }

                try
                {
                    Log.Debug("checking SiteCode...");
                    masterFacility = await _patientExtractRepository.VerifyFacility(manifest.SiteCode);
                    if (null == masterFacility)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                            new HttpError($"SiteCode [{manifest.SiteCode}] NOT FOUND in Master Facility List"));
                }
                catch (Exception e)
                {
                    Log.Error("SiteCode Error",e);
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
                }

                try
                {
                    Log.Debug("checking SiteCode Enrollment...");
                    _facilityRepository.Enroll(masterFacility, manifest.EmrName, Properties.Settings.Default.AllowSnapshot);
                }
                catch (Exception e)
                {
                    Log.Error("SiteCode Enroll Error",e);
                }

                try
                {

                    Log.Debug("clearing Site Manifest...");
                    await _patientExtractRepository.ClearManifest(manifest);
                }
                catch (Exception e)
                {
                    Log.Error("Clear Site Manifest Error",e);
                }

                try
                {
                    if (manifest.UploadMode == UploadMode.Normal)
                    {
                        Log.Debug("removing Site Duplicates...");
                        await _patientExtractRepository.RemoveDuplicates(manifest.SiteCode);
                    }
                }
                catch (Exception e)
                {
                    Log.Error("Removing Site Duplicates Error", e);
                }

                try
                {
                    Log.Debug("posting to SPOT...");
                    var facManifest = FacilityManifest.Create(manifest);
                    var manifestDto = new ManifestDto(masterFacility, facManifest);
                    var metrics = MetricDto.Generate(masterFacility, facManifest);
                    var metricDtos = metrics.Where(x => x.CargoType != CargoType.Indicator).ToList();
                    var indicatorDtos = metrics.Where(x => x.CargoType == CargoType.Indicator).ToList();
                    var result = await _liveSyncService.SyncManifest(manifestDto);
                    
                    if (metricDtos.Any())
                        await _liveSyncService.SyncMetrics(metricDtos);
                    if (indicatorDtos.Any())
                    {
                        var indstats = IndicatorDto.Generate(indicatorDtos);
                        await _liveSyncService.SyncIndicators(indstats);
                    }
                }
                catch (Exception e)
                {
                    Log.Error("Posting to SPOT Error", e);
                }

                try
                {
                    Log.Debug("sending to Queue");
                    await _messagingService.SendAsync(manifest, _gateway);
                    return Request.CreateResponse(HttpStatusCode.OK, $"{masterFacility}");
                }
                catch (Exception ex)
                {
                    Log.Error("Sending to QueueError",ex);
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError($"The expected '{new Manifest().GetType().Name}' is null"));
        }
    }
}
