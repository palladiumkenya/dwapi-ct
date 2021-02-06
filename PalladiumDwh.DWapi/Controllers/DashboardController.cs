using System.Collections.Generic;
using System.Web.Http.Description;
using System.Web.Mvc;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.DWapi.Models;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.DWapi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DashboardController : Controller
    {
        private readonly IMessagingSenderService _service;

        public DashboardController(IMessagingSenderService messagingReaderService)
        {
            _service = messagingReaderService;
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            List<Queues> list=new List<Queues>();

            var gateways = GenerateGatewayList();

            foreach (var g in gateways)
            {

                list.Add(new Queues(g, _service.GetNumberOfMessages(g), _service.GetNumberOfMessages(g,true)));
            }
            
            return View(list);
        }

        public ActionResult PurgeAll()
        {
            var gateways = GenerateGatewayList();

            foreach (var g in gateways)
            {
                _service.Purge(g);
                _service.Purge(g,true);
            }
            return RedirectToAction("Index");
        }

        public List<string> GenerateGatewayList()
        {
            string queueName = Properties.Settings.Default.QueueName;

            var gateways = new List<string>
            {
                $"{queueName}.{nameof(Manifest).ToLower()}"
            };

            var baseGateways = new List<string>
            {
                $"{queueName}.{nameof(PatientARTProfile).ToLower()}",
                $"{queueName}.{nameof(PatientBaselineProfile).ToLower()}",
                $"{queueName}.{nameof(PatientLabProfile).ToLower()}",
                $"{queueName}.{nameof(PatientPharmacyProfile).ToLower()}",
                $"{queueName}.{nameof(PatientVisitProfile).ToLower()}",
                $"{queueName}.{nameof(PatientStatusProfile).ToLower()}",
                $"{queueName}.{nameof(PatientAdverseEventProfile).ToLower()}",
            };

            gateways.AddRange(baseGateways);

            foreach (var gateway in baseGateways)
            {
                gateways.Add($"{gateway}.batch");
            }

            return gateways;
        }
    }
}