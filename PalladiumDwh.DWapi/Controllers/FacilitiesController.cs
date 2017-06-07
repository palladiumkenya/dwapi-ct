using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using log4net;
using PalladiumDwh.Core.Interfaces;

namespace PalladiumDwh.DWapi.Controllers
{
    public class FacilitiesController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ISyncService _syncService;

        public FacilitiesController(ISyncService syncService)
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
                response = Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Facility with Code:{id} not found!");
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, facility.GetStatus());
            }
            return response;
        }
    }
}
