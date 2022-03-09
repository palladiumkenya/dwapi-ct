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

        public SyncOvcHandler(IMapper mapper, IStageOvcExtractRepository stageRepository,
            IFacilityRepository facilityRepository)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<Unit> Handle(SyncOvc request, CancellationToken cancellationToken)
        {
            try
            {
                await _stageRepository.ClearSite(request.OvcSourceBag.FacilityId.Value, request.OvcSourceBag.ManifestId.Value);
                var extracts = _mapper.Map<List<StageOvcExtract>>(request.OvcSourceBag.Extracts);
                if (request.OvcSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.OvcSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.OvcSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.OvcSourceBag.ManifestId.Value);
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
