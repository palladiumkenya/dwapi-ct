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
        private readonly IMediator _mediator;

        public SyncGbvScreeningHandler(IMapper mapper, IStageGbvScreeningExtractRepository stageRepository,
            IFacilityRepository facilityRepository, IMediator mediator)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(SyncGbvScreening request, CancellationToken cancellationToken)
        {
            try
            {
                var extracts = _mapper.Map<List<StageGbvScreeningExtract>>(request.GbvScreeningSourceBag.Extracts);
                if (request.GbvScreeningSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (request.GbvScreeningSourceBag.FacilityId.IsNullOrEmpty())
                    {
                        var facs = _facilityRepository.ReadFacilityCache();
                        request.GbvScreeningSourceBag.SetFacility(facs);
                    }

                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.GbvScreeningSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.GbvScreeningSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.GbvScreeningSourceBag.ManifestId.Value);

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
