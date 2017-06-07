using System.Collections.Generic;
using System.Web.Mvc;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.DWapi.Models;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.DWapi.Controllers
{
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

            string queueName = Properties.Settings.Default.QueueName;

            var gateways = new List<string>
            {
                $"{queueName}.{typeof(PatientARTProfile).Name.ToLower()}",
                $"{queueName}.{typeof(PatientBaselineProfile).Name.ToLower()}",
                $"{queueName}.{typeof(PatientLabProfile).Name.ToLower()}",
                $"{queueName}.{typeof(PatientPharmacyProfile).Name.ToLower()}",
                $"{queueName}.{typeof(PatientVisitProfile).Name.ToLower()}",
                $"{queueName}.{typeof(PatientStatusProfile).Name.ToLower()}",
                $"{queueName}.{typeof(Manifest).Name.ToLower()}"
            };


            foreach (var g in gateways)
            {
                list.Add(new Queues(g, _service.GetNumberOfMessages(g), _service.GetNumberOfMessages(g,true)));
            }
            
            return View(list);
        }


        public ActionResult PurgeAll()
        {
            string queueName = Properties.Settings.Default.QueueName;

            var gateways = new List<string>
            {
                $"{queueName}.{typeof(PatientARTProfile).Name.ToLower()}",
                $"{queueName}.{typeof(PatientBaselineProfile).Name.ToLower()}",
                $"{queueName}.{typeof(PatientLabProfile).Name.ToLower()}",
                $"{queueName}.{typeof(PatientPharmacyProfile).Name.ToLower()}",
                $"{queueName}.{typeof(PatientVisitProfile).Name.ToLower()}",
                $"{queueName}.{typeof(PatientStatusProfile).Name.ToLower()}",
                $"{queueName}.{typeof(Manifest).Name.ToLower()}"
            };

            foreach (var g in gateways)
            {
                _service.Purge(g);
                _service.Purge(g,true);
            }
            return RedirectToAction("Index");
        }
    }
}