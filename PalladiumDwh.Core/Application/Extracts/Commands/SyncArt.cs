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
    public class SyncArt : IRequest
    {
        public ArtSourceBag ArtSourceBag { get; }

        public SyncArt(ArtSourceBag artSourceBag)
        {
            ArtSourceBag = artSourceBag;
        }
    }

    public class SyncArtHandler : IRequestHandler<SyncArt>
    {
        private readonly IMapper _mapper;
        private readonly IStageArtExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IMediator _mediator;

        public SyncArtHandler(IMapper mapper, IStageArtExtractRepository stageRepository,
            IFacilityRepository facilityRepository, IMediator mediator)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(SyncArt request, CancellationToken cancellationToken)
        {
            try
            {
           //     await _stageRepository.ClearSite(request.ArtSourceBag.FacilityId.Value, request.ArtSourceBag.ManifestId.Value);

                var extracts = _mapper.Map<List<StageArtExtract>>(request.ArtSourceBag.Extracts);
                if (request.ArtSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (request.ArtSourceBag.FacilityId.IsNullOrEmpty())
                    {
                        var facs = _facilityRepository.ReadFacilityCache();
                        request.ArtSourceBag.SetFacility(facs);
                    }

                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.ArtSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.ArtSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.ArtSourceBag.ManifestId.Value);

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
