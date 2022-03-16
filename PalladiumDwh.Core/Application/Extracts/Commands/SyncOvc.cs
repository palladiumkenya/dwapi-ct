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
    public class SyncOvc : IRequest
    {
        public OvcSourceBag OvcSourceBag { get; }

        public SyncOvc(OvcSourceBag ovcSourceBag)
        {
            OvcSourceBag = ovcSourceBag;
        }
    }

    public class SyncOvcHandler : IRequestHandler<SyncOvc>
    {
        private readonly IMapper _mapper;
        private readonly IStageOvcExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IMediator _mediator;

        public SyncOvcHandler(IMapper mapper, IStageOvcExtractRepository stageRepository,
            IFacilityRepository facilityRepository, IMediator mediator)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(SyncOvc request, CancellationToken cancellationToken)
        {
            try
            {

               // await _stageRepository.ClearSite(request.OvcSourceBag.FacilityId.Value, request.OvcSourceBag.ManifestId.Value);
                var extracts = _mapper.Map<List<StageOvcExtract>>(request.OvcSourceBag.Extracts);
                if (request.OvcSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (request.OvcSourceBag.FacilityId.IsNullOrEmpty())
                    {
                        var facs = _facilityRepository.ReadFacilityCache();
                        request.OvcSourceBag.SetFacility(facs);
                    }

                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.OvcSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.OvcSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.OvcSourceBag.ManifestId.Value);

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
