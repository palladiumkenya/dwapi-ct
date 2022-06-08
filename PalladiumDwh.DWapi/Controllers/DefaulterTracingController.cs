using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using Hangfire;
using log4net;
using MediatR;
using PalladiumDwh.Core.Application.Commands;
using PalladiumDwh.Core.Application.Extracts.Commands;
using PalladiumDwh.Core.Application.Extracts.Source;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.DWapi.Controllers
{
    public class DefaulterTracingController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IMessagingSenderService _messagingService;
        private readonly string _gateway = typeof(DefaulterTracingProfile).Name.ToLower();
        private readonly string _gatewayBatch;
        private readonly IMessengerScheduler _messengerScheduler;
        private readonly IMediator _mediator;
        public DefaulterTracingController(IMessagingSenderService messagingService,IMessengerScheduler messengerScheduler, IMediator mediator)
        {
            _messagingService = messagingService;
            _messengerScheduler = messengerScheduler;
            _mediator = mediator;
            _messagingService.Initialize(_gateway);
            _gatewayBatch = $"{_gateway}.batch";
            _messagingService.Initialize(_gatewayBatch);
        }

        // public async Task<HttpResponseMessage> Post([FromBody] DefaulterTracingProfile patientProfile)
        // {
        //     if (null != patientProfile)
        //     {
        //         if (!patientProfile.IsValid())
        //         {
        //             return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
        //                 new HttpError("Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
        //         }
        //         try
        //         {
        //             patientProfile.GeneratePatientRecord();
        //             var messageRef = await _messagingService.SendAsync(patientProfile, _gateway);
        //             return Request.CreateResponse(HttpStatusCode.OK, $"{messageRef}");
        //         }
        //         catch (Exception ex)
        //         {
        //             Log.Error(new string('*',30));
        //             Log.Error(nameof(DefaulterTracingProfile),ex);
        //             Log.Error(new string('*',30));
        //             return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //         }
        //     }
        //     return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError($"The expected '{new DefaulterTracingProfile().GetType().Name}' is null"));
        // }

        [Route("api/v2/DefaulterTracing")]
        public async Task<HttpResponseMessage> PostBatch([FromBody] List<DefaulterTracingProfile> patientProfile)
        {
            if (null != patientProfile && patientProfile.Any())
            {
                if (patientProfile.Any(x => !x.IsValid()))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        new HttpError(
                            "Invalid data,Please ensure its has Patient,Facility and atleast one (1) Extract"));
                }

                try
                {
                    var messageRef =
                        await _messagingService.SendAsync(patientProfile, _gatewayBatch, patientProfile.GetType());
                    return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                        new { BatchKey = new List<Guid>() {LiveGuid.NewGuid()}});
                }
                catch (Exception ex)
                {
                    Log.Error(new string('*',30));
                    Log.Error(nameof(DefaulterTracingProfile),ex);
                    Log.Error(new string('*',30));
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                new HttpError($"The expected '{new DefaulterTracingProfile().GetType().Name}' is null"));
        }

        [Route("api/v3/DefaulterTracing")]
        public async Task<HttpResponseMessage> PostBatchNew([FromBody] DefaulterTracingSourceBag sourceBag)
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
                            x => { x.Enqueue(() => Send($"{sourceBag}", new SyncDefaulterTracing(sourceBag))); });
                    }
                    else
                    {
                         jobId = BatchJob.StartNew(x =>
                        {
                            x.Enqueue(() => Send($"{sourceBag}", new SyncDefaulterTracing(sourceBag)));
                        });
                    }

                    return Request.CreateResponse<dynamic>(HttpStatusCode.OK,
                        new
                        {
                            JobId=jobId,
                            BatchKey = new List<Guid>() {LiveGuid.NewGuid()}
                        });
                }
                catch (Exception ex)
                {
                    Log.Error(new string('*', 30));
                    Log.Error(nameof(DefaulterTracingSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                new HttpError($"The expected '{new PatientLabProfile().GetType().Name}' is null"));
        }
        [Queue("gamma")]
        [DisableConcurrentExecution(10 * 60)]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task Send(string jobName, IRequest command)
        {
            await _mediator.Send(command);
        }
    }
}
