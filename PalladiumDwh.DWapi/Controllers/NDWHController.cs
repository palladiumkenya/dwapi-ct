using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using log4net;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.DWapi.Models;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.DWapi.Controllers
{
    public class NdwhController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IPatientExtractRepository _patientExtractRepository;
        private readonly IMessagingSenderService _messagingService;
        private readonly string _gateway = typeof(Manifest).Name.ToLower();

        public NdwhController(IMessagingSenderService messagingService, IPatientExtractRepository patientExtractRepository)
        {
            _messagingService = messagingService;
            _messagingService.Initialize(_gateway);
            _patientExtractRepository = patientExtractRepository;
        }

        [HttpPost]
        public IHttpActionResult Verify([FromBody] Subscriber subscriber)
        {
            if (null == subscriber)
            {
                return BadRequest();
            }

            if (subscriber.Verify())
                    return Ok(new
                    {
                        registryName= "National DataWarehouse"
                    });

            return Unauthorized();
        }
    }
}