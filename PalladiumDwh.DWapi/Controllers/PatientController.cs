using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Hangfire;
using log4net;
using MediatR;
using PalladiumDwh.Core.Application.Commands;
using PalladiumDwh.Core.Application.Extracts.Commands;
using PalladiumDwh.Core.Application.Extracts.Source;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;
using PalladiumDwh.DWapi.Helpers;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.DWapi.Controllers
{
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class PatientController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IMessagingSenderService _messagingService;
        private readonly string _gateway = typeof(PatientARTProfile).Name.ToLower();
        private readonly ISyncService _syncService;
        private readonly IMediator _mediator;

        public PatientController(IMessagingSenderService messagingService, ISyncService syncService, IMediator mediator)
        {
            _messagingService = messagingService;
            _syncService = syncService;
            _mediator = mediator;
        }

        // public async Task<HttpResponseMessage> Post([FromBody] SitePatientProfile patientProfile)
        // {
        //     if (null != patientProfile)
        //     {
        //         if (!patientProfile.IsValid())
        //         {
        //             return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
        //                 new HttpError(
        //                     "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
        //         }
        //
        //         try
        //         {
        //             var messageRef = await _messagingService.SendAsync(patientProfile, _gateway);
        //             return Request.CreateResponse(HttpStatusCode.OK, $"{messageRef}");
        //         }
        //         catch (Exception ex)
        //         {
        //             Log.Debug(ex);
        //             return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //         }
        //     }
        //
        //     return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
        //         new HttpError($"The expected '{new PatientARTProfile().GetType().Name}' is null"));
        // }

        [Route("api/v3/Patient")]
        public async Task<HttpResponseMessage> PostBatch([FromBody] PatientSourceBag sourceBag)
        {

            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new HttpError(
                            "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                }

                try
                {
                    string jobId;
                    if (sourceBag.HasJobId)
                    {
                        jobId = BatchJob.ContinueBatchWith(sourceBag.JobId,
                            x => { x.Enqueue(() => Send($"{sourceBag}", new SyncPatient(sourceBag))); },$"{sourceBag}");
                    }
                    else
                    {
                        jobId = BatchJob.StartNew(x =>
                        {
                            x.Enqueue(() => Send($"{sourceBag}", new SyncPatient(sourceBag)));
                        },$"{sourceBag}");
                    }


                    return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                        new
                        {
                            JobId = jobId,
                            BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                        });
                }
                catch (Exception ex)
                {
                    Log.Error(new string('*', 30));
                    Log.Error(nameof(PatientSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                new HttpError($"The expected '{new PatientSourceBag().GetType().Name}' is null"));
        }
        [Queue("alpha")]
       // [DisableConcurrentExecution(10*60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task Send(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }
    }
}