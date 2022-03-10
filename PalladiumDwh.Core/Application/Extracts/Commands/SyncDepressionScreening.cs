using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PalladiumDwh.Core.Application.Extracts.Source;
using PalladiumDwh.Core.Application.Extracts.Stage;
using PalladiumDwh.Core.Application.Extracts.Stage.Repositories;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Extentions;

namespace PalladiumDwh.Core.Application.Extracts.Commands
{

    public class SyncDepressionScreening : IRequest
    {
        public DepressionScreeningSourceBag DepressionScreeningSourceBag { get; }

        public SyncDepressionScreening(DepressionScreeningSourceBag depressionScreeningSourceBag)
        {
            DepressionScreeningSourceBag = depressionScreeningSourceBag;
        }
    }

    public class SyncDepressionScreeningHandler : IRequestHandler<SyncDepressionScreening>
    {
        private readonly IMapper _mapper;
        private readonly IStageDepressionScreeningExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;

        public SyncDepressionScreeningHandler(IMapper mapper,
            IStageDepressionScreeningExtractRepository stageRepository,
            IFacilityRepository facilityRepository)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<Unit> Handle(SyncDepressionScreening request, CancellationToken cancellationToken)
        {
            try
            {
                await _stageRepository.ClearSite(request.DepressionScreeningSourceBag.FacilityId.Value, request.DepressionScreeningSourceBag.ManifestId.Value);

                var extracts =
                    _mapper.Map<List<StageDepressionScreeningExtract>>(request.DepressionScreeningSourceBag.Extracts);
                if (request.DepressionScreeningSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (request.DepressionScreeningSourceBag.FacilityId.IsNullOrEmpty())
                    {
                        var facs = _facilityRepository.ReadFacilityCache();
                        request.DepressionScreeningSourceBag.SetFacility(facs);
                    }

                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.DepressionScreeningSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any())
                        extracts.ForEach(x => x.Standardize(request.DepressionScreeningSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.DepressionScreeningSourceBag.ManifestId.Value);
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
