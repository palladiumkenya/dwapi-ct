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
        
        private readonly IMessagingService _messagingService;

        public PatientArtController(IMessagingService messagingService)
        {
     
            _messagingService = messagingService;
            _messagingService.Initialize();
        }

        public HttpResponseMessage Post([FromBody] PatientARTProfile patientProfile)
        {
            try
            {
                patientProfile.GeneratePatientRecord();
                var messageRef=_messagingService.Send(patientProfile);                
                return Request.CreateResponse(HttpStatusCode.OK, $"{messageRef}");
            }
            catch (Exception ex)
            {
                var body = Request.Content.ReadAsStringAsync().Result;
                Log.Debug(ex);
                Log.Debug(body);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
