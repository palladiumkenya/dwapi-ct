using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PalladiumDwh.Core.Application.Source;
using PalladiumDwh.Core.Application.Stage.Repositories;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Enum;

namespace PalladiumDwh.Core.Application.Commands
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

        public SyncAdverseEventHandler(IMapper mapper, IStageAdverseEventExtractRepository stageRepository,
            IFacilityRepository facilityRepository)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<Unit> Handle(SyncAdverseEvent request, CancellationToken cancellationToken)
        {
            try
            {
                await _stageRepository.ClearSite(request.AdverseEventSourceBag.FacilityId.Value, request.AdverseEventSourceBag.ManifestId.Value);

                var extracts = _mapper.Map<List<StageAdverseEventExtract>>(request.AdverseEventSourceBag.Extracts);
                if (request.AdverseEventSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.AdverseEventSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.AdverseEventSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.AdverseEventSourceBag.ManifestId.Value);
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
