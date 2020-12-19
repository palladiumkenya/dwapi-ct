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
    public class PatientLabsController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IMessagingSenderService _messagingService;
        private readonly string _gateway = typeof(PatientLabProfile).Name.ToLower();
        private readonly string _gatewayBatch;
        private readonly IMessengerScheduler _messengerScheduler;
        public PatientLabsController(IMessagingSenderService messagingService,IMessengerScheduler messengerScheduler)
        {
            _messagingService = messagingService;
            _messengerScheduler = messengerScheduler;
            _messagingService.Initialize(_gateway);
            _gatewayBatch = $"{_gateway}.batch";
            _messagingService.Initialize(_gatewayBatch);
        }
        public async Task<HttpResponseMessage> Post([FromBody] PatientLabProfile patientProfile)
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
                    var messageRef = await _messagingService.SendAsync(patientProfile,_gateway);
                    return Request.CreateResponse(HttpStatusCode.OK, $"{messageRef}");
                }
                catch (Exception ex)
                {
                    Log.Debug(ex);
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError($"The expected '{new PatientLabProfile().GetType().Name}' is null"));
        }

        [Route("api/v2/PatientLabs")]
        public async Task<HttpResponseMessage> PostBatch([FromBody] List<PatientLabProfile> patientProfile)
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
                    var messageRef =
                        await _messagingService.SendAsync(patientProfile, _gatewayBatch, patientProfile.GetType());
                    return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                        new {BatchKey = messageRef});
                }
                catch (Exception ex)
                {
                    Log.Debug(ex);
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                new HttpError($"The expected '{new PatientLabProfile().GetType().Name}' is null"));
        }
    }
}
