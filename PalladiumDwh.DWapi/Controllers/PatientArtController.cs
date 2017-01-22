using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using log4net;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Core.Model.Profiles;

namespace PalladiumDwh.DWapi.Controllers
{
    public class PatientArtController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IMessagingSenderService _messagingService;
        private readonly string _gateway = typeof(PatientARTProfile).Name.ToLower();

        public PatientArtController(IMessagingSenderService messagingService)
        {
            _messagingService = messagingService;
            _messagingService.Initialize(_gateway);
        }

        public HttpResponseMessage Post([FromBody] PatientARTProfile patientProfile)
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
                    var messageRef = _messagingService.Send(patientProfile, _gateway);
                    return Request.CreateResponse(HttpStatusCode.OK, $"{messageRef}");
                }
                catch (Exception ex)
                {
                    Log.Debug(ex);
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError($"The expected '{new PatientARTProfile().GetType().Name}' is null") );
        }
    }
}
