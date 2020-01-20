using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using log4net;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Model.Profile;


namespace PalladiumDwh.DWapi.Controllers
{
    public class PatientBaselinesController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IMessagingSenderService _messagingService;
        private readonly string _gateway = typeof(PatientBaselineProfile).Name.ToLower();

        public PatientBaselinesController(IMessagingSenderService messagingService)
        {
            _messagingService = messagingService;
            _messagingService.Initialize(_gateway);
        }

        public async Task<HttpResponseMessage> Post([FromBody] PatientBaselineProfile patientProfile)
        {

            if (null != patientProfile)
            {
                if (!patientProfile.IsValid())
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new HttpError("Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                }
                try
                {
                    patientProfile.GeneratePatientRecord();
                    var messageRef = await _messagingService.SendAsync(patientProfile, _gateway);
                    return Request.CreateResponse(HttpStatusCode.OK, $"{messageRef}");
                }
                catch (Exception ex)
                {
                    Log.Debug(ex);
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError($"The expected '{new PatientBaselineProfile().GetType().Name}' is null"));
        }

        [HttpPost]
        [ActionName("batch")]
        public async Task<HttpResponseMessage> PostBatch([FromBody] List<PatientAdverseEventProfile> patientProfile)
        {
            if (null != patientProfile && patientProfile.Any())
            {
                if (patientProfile.Any(x => !x.IsValid()))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new HttpError(
                            "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                }

                try
                {
                    var messageRef = await _messagingService.SendBatchAsync(patientProfile, _gateway);
                    return Request.CreateResponse(HttpStatusCode.OK, $"{messageRef}");
                }
                catch (Exception ex)
                {
                    Log.Debug(ex);
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                new HttpError($"The expected '{new PatientBaselineProfile().GetType().Name}' is null"));
        }
    }
}
