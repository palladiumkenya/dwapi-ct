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

    public class SyncEnhancedAdherenceCounselling : IRequest
    {
        public EnhancedAdherenceCounsellingSourceBag EnhancedAdherenceCounsellingSourceBag { get; }

        public SyncEnhancedAdherenceCounselling(
            EnhancedAdherenceCounsellingSourceBag enhancedAdherenceCounsellingSourceBag)
        {
            EnhancedAdherenceCounsellingSourceBag = enhancedAdherenceCounsellingSourceBag;
        }
    }

    public class SyncEnhancedAdherenceCounsellingHandler : IRequestHandler<SyncEnhancedAdherenceCounselling>
    {
        private readonly IMapper _mapper;
        private readonly IStageEnhancedAdherenceCounsellingExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;

        public SyncEnhancedAdherenceCounsellingHandler(IMapper mapper,
            IStageEnhancedAdherenceCounsellingExtractRepository stageRepository,
            IFacilityRepository facilityRepository)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<Unit> Handle(SyncEnhancedAdherenceCounselling request, CancellationToken cancellationToken)
        {
            try
            {

                await _stageRepository.ClearSite(request.EnhancedAdherenceCounsellingSourceBag.FacilityId.Value, request.EnhancedAdherenceCounsellingSourceBag.ManifestId.Value);

                var extracts =
                    _mapper.Map<List<StageEnhancedAdherenceCounsellingExtract>>(request
                        .EnhancedAdherenceCounsellingSourceBag.Extracts);
                if (request.EnhancedAdherenceCounsellingSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (extracts.Any())
                        extracts.ForEach(x => x.Standardize(request.EnhancedAdherenceCounsellingSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any())
                        extracts.ForEach(x => x.Standardize(request.EnhancedAdherenceCounsellingSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts,
                    request.EnhancedAdherenceCounsellingSourceBag.ManifestId.Value);
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
