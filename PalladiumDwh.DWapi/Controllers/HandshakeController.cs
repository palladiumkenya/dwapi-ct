using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using PalladiumDwh.Shared.Model.Profile;
using log4net;
using PalladiumDwh.Core.Interfaces;

namespace PalladiumDwh.DWapi.Controllers
{
    public class HandshakeController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IFacilityRepository _facilityRepository;
        private readonly ILiveSyncService _liveSyncService;
        public HandshakeController(IFacilityRepository facilityRepository, ILiveSyncService liveSyncService)
        {
            _facilityRepository = facilityRepository;
            _liveSyncService = liveSyncService;
        }

        public async Task<HttpResponseMessage> Post(Guid session)
        {
            try
            {
                _facilityRepository.EndSession(session);
                var handshakes = _facilityRepository.GetSessionHandshakes(session).ToList();
                await _liveSyncService.SyncHandshake(handshakes);
                return Request.CreateResponse(HttpStatusCode.OK, $"{session}");
            }
            catch (Exception e)
            {
                Log.Error("Handshake error", e);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}
