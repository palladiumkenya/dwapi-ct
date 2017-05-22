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
    public class ArtController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IMessagingSenderService _messagingService;
        private readonly string _gateway = typeof(PatientARTProfile).Name.ToLower();

        public ArtController(IMessagingSenderService messagingService)
        {
            _messagingService = messagingService;
            _messagingService.Initialize(_gateway);
        }

        public async Task<HttpResponseMessage> Post()
        {
            var re = await Request.Content.ReadAsStringAsync();
            var req = await Request.Content.ReadAsByteArrayAsync();
            var req2 = await Request.Content.ReadAsStreamAsync();
            
            return Request.CreateResponse(HttpStatusCode.OK, $"MAUN!");

        }
    }
}
