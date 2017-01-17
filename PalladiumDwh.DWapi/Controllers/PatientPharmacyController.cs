using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using log4net;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model.Profiles;

namespace PalladiumDwh.DWapi.Controllers
{
    public class PatientPharmacyController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ISyncService _syncService;

        public PatientPharmacyController(ISyncService syncService)
        {
            _syncService = syncService;
        }

        public HttpResponseMessage Post([FromBody] PatientPharmacyProfile patientProfile)
        {
            try
            {
                _syncService.SyncPharmacy(patientProfile);
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
