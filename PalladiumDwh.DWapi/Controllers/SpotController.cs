using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using log4net;
using Newtonsoft.Json;
using PalladiumDwh.Core.Exchange;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Profile;

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
                    masterFacility = await _patientExtractRepository.VerifyFacility(manifest.SiteCode);
                    if (null == masterFacility)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                            new HttpError($"SiteCode [{manifest.SiteCode}] NOT FOUND in Master Facility List"));


                }
                catch (Exception e)
                {
                    Log.Debug(e);
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
                }

                try
                {
                    _facilityRepository.Enroll(masterFacility);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }

                try
                {
                    await _patientExtractRepository.ClearManifest(manifest);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }

                try
                {
                    await _patientExtractRepository.RemoveDuplicates(manifest.SiteCode);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }

                try
                {
                    await _patientExtractRepository.InitializeManifest(manifest);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }

                try
                {
                    var facManifest = FacilityManifest.Create(manifest);
                    var manifestDto = new ManifestDto(masterFacility, facManifest);
                    var result= await _liveSyncService.SyncManifest(manifestDto);
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }

                try
                {
                    await _messagingService.SendAsync(manifest, _gateway);
                    return Request.CreateResponse(HttpStatusCode.OK, $"{masterFacility}");
                }
                catch (Exception ex)
                {
                    Log.Debug(ex);
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError($"The expected '{new Manifest().GetType().Name}' is null"));
        }
    }
}
