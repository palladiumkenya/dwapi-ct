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
        private readonly ISyncService _syncService;

        public PatientArtController(ISyncService syncService)
        {
            _syncService = syncService;
        }

        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage response = null;
            var facility = _syncService.GetFacility(id);
            
            if (facility == null)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Facility not found!");

            }
            else
            {
                response = Request.CreateResponse<Facility>(HttpStatusCode.OK, facility);
            }
            return response;
        }

        public HttpResponseMessage Post([FromBody] PatientARTProfile patientProfile)
        {
            try
            {
                _syncService.SyncArt(patientProfile);
                return Request.CreateResponse(HttpStatusCode.OK, $"{patientProfile}");
            }
            catch (Exception ex)
            {
                Log.Debug(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
