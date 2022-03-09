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
    public class SyncDefaulterTracing : IRequest
    {
        public DefaulterTracingSourceBag DefaulterTracingSourceBag { get; }

        public SyncDefaulterTracing(DefaulterTracingSourceBag defaulterTracingSourceBag)
        {
            DefaulterTracingSourceBag = defaulterTracingSourceBag;
        }
    }

    public class SyncDefaulterTracingHandler : IRequestHandler<SyncDefaulterTracing>
    {
        private readonly IMapper _mapper;
        private readonly IStageDefaulterTracingExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;

        public SyncDefaulterTracingHandler(IMapper mapper, IStageDefaulterTracingExtractRepository stageRepository,
            IFacilityRepository facilityRepository)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<Unit> Handle(SyncDefaulterTracing request, CancellationToken cancellationToken)
        {
            try
            {
                await _stageRepository.ClearSite(request.DefaulterTracingSourceBag.FacilityId.Value, request.DefaulterTracingSourceBag.ManifestId.Value);

                var extracts =
                    _mapper.Map<List<StageDefaulterTracingExtract>>(request.DefaulterTracingSourceBag.Extracts);
                if (request.DefaulterTracingSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.DefaulterTracingSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.DefaulterTracingSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.DefaulterTracingSourceBag.ManifestId.Value);
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
