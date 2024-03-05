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

    public class SyncRelationships : IRequest
    {
        public RelationshipsSourceBag RelationshipsSourceBag { get; }

        public SyncRelationships(RelationshipsSourceBag relationshipsSourceBag)
        {
            RelationshipsSourceBag = relationshipsSourceBag;
        }
    }

    public class SyncRelationshipsHandler : IRequestHandler<SyncRelationships>
    {
        private readonly IMapper _mapper;
        private readonly IStageRelationshipsExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IMediator _mediator;

        public SyncRelationshipsHandler(IMapper mapper,
            IStageRelationshipsExtractRepository stageRepository,
            IFacilityRepository facilityRepository, IMediator mediator)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(SyncRelationships request, CancellationToken cancellationToken)
        {
            try
            {
                // await _stageRepository.ClearSite(request.RelationshipsSourceBag.FacilityId.Value, request.RelationshipsSourceBag.ManifestId.Value);

                var extracts =
                    _mapper.Map<List<StageRelationshipsExtract>>(request.RelationshipsSourceBag
                        .Extracts);
                if (request.RelationshipsSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (request.RelationshipsSourceBag.FacilityId.IsNullOrEmpty())
                    {
                        var facs = _facilityRepository.ReadFacilityCache();
                        request.RelationshipsSourceBag.SetFacility(facs);
                    }

                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.RelationshipsSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any())
                        extracts.ForEach(x => x.Standardize(request.RelationshipsSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.RelationshipsSourceBag.ManifestId.Value);

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
