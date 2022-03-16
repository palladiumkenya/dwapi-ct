using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PalladiumDwh.Core.Application.Extracts.Notififactions;
using PalladiumDwh.Core.Application.Extracts.Source;
using PalladiumDwh.Core.Application.Extracts.Stage;
using PalladiumDwh.Core.Application.Extracts.Stage.Repositories;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Extentions;

namespace PalladiumDwh.Core.Application.Extracts.Commands
{
    public class SyncAdverseEvent : IRequest
    {
        public AdverseEventSourceBag AdverseEventSourceBag { get; }

        public SyncAdverseEvent(AdverseEventSourceBag adverseEventSourceBag)
        {
            AdverseEventSourceBag = adverseEventSourceBag;
        }
    }

    public class SyncAdverseEventHandler : IRequestHandler<SyncAdverseEvent>
    {
        private readonly IMapper _mapper;
        private readonly IStageAdverseEventExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IMediator _mediator;

        public SyncAdverseEventHandler(IMapper mapper, IStageAdverseEventExtractRepository stageRepository,
            IFacilityRepository facilityRepository, IMediator mediator)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(SyncAdverseEvent request, CancellationToken cancellationToken)
        {
            try
            {

               // await _stageRepository.ClearSite(request.AdverseEventSourceBag.FacilityId.Value,
                 //   request.AdverseEventSourceBag.ManifestId.Value);

                var extracts = _mapper.Map<List<StageAdverseEventExtract>>(request.AdverseEventSourceBag.Extracts);
                if (request.AdverseEventSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (request.AdverseEventSourceBag.FacilityId.IsNullOrEmpty())
                    {
                        var facs = _facilityRepository.ReadFacilityCache();
                        request.AdverseEventSourceBag.SetFacility(facs);
                    }

                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.AdverseEventSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.AdverseEventSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.AdverseEventSourceBag.ManifestId.Value);

                var facIds = extracts.Select(x => x.FacilityId).Distinct().ToList();
                await _mediator.Publish(new SyncExtractEvent(facIds), cancellationToken);
                return Unit.Value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
