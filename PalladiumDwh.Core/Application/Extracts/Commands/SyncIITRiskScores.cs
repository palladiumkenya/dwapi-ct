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

    public class SyncIITRiskScores : IRequest
    {
        public IITRiskScoresSourceBag IITRiskScoresSourceBag { get; }

        public SyncIITRiskScores(IITRiskScoresSourceBag iitRiskScoresSourceBag)
        {
            IITRiskScoresSourceBag = iitRiskScoresSourceBag;
        }
    }

    public class SyncIITRiskScoresHandler : IRequestHandler<SyncIITRiskScores>
    {
        private readonly IMapper _mapper;
        private readonly IStageIITRiskScoresExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IMediator _mediator;

        public SyncIITRiskScoresHandler(IMapper mapper,
            IStageIITRiskScoresExtractRepository stageRepository,
            IFacilityRepository facilityRepository, IMediator mediator)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(SyncIITRiskScores request, CancellationToken cancellationToken)
        {
            try
            {
                // await _stageRepository.ClearSite(request.IITRiskScoresSourceBag.FacilityId.Value, request.IITRiskScoresSourceBag.ManifestId.Value);

                var extracts =
                    _mapper.Map<List<StageIITRiskScoresExtract>>(request.IITRiskScoresSourceBag
                        .Extracts);
                if (request.IITRiskScoresSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (request.IITRiskScoresSourceBag.FacilityId.IsNullOrEmpty())
                    {
                        var facs = _facilityRepository.ReadFacilityCache();
                        request.IITRiskScoresSourceBag.SetFacility(facs);
                    }

                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.IITRiskScoresSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any())
                        extracts.ForEach(x => x.Standardize(request.IITRiskScoresSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.IITRiskScoresSourceBag.ManifestId.Value);

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
