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
    public class SyncGbvScreening : IRequest
    {
        public GbvScreeningSourceBag GbvScreeningSourceBag { get; }

        public SyncGbvScreening(GbvScreeningSourceBag gbvScreeningSourceBag)
        {
            GbvScreeningSourceBag = gbvScreeningSourceBag;
        }
    }

    public class SyncGbvScreeningHandler : IRequestHandler<SyncGbvScreening>
    {
        private readonly IMapper _mapper;
        private readonly IStageGbvScreeningExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;

        public SyncGbvScreeningHandler(IMapper mapper, IStageGbvScreeningExtractRepository stageRepository,
            IFacilityRepository facilityRepository)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<Unit> Handle(SyncGbvScreening request, CancellationToken cancellationToken)
        {
            try
            {
                var extracts = _mapper.Map<List<StageGbvScreeningExtract>>(request.GbvScreeningSourceBag.Extracts);
                if (request.GbvScreeningSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.GbvScreeningSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.GbvScreeningSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.GbvScreeningSourceBag.ManifestId.Value);
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
