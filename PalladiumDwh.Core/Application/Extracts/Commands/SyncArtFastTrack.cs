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

    public class SyncArtFastTrack : IRequest
    {
        public ArtFastTrackSourceBag ArtFastTrackSourceBag { get; }

        public SyncArtFastTrack(ArtFastTrackSourceBag artFastTrackSourceBag)
        {
            ArtFastTrackSourceBag = artFastTrackSourceBag;
        }
    }

    public class SyncArtFastTrackHandler : IRequestHandler<SyncArtFastTrack>
    {
        private readonly IMapper _mapper;
        private readonly IStageArtFastTrackExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IMediator _mediator;

        public SyncArtFastTrackHandler(IMapper mapper,
            IStageArtFastTrackExtractRepository stageRepository,
            IFacilityRepository facilityRepository, IMediator mediator)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(SyncArtFastTrack request, CancellationToken cancellationToken)
        {
            try
            {
                // await _stageRepository.ClearSite(request.ArtFastTrackSourceBag.FacilityId.Value, request.ArtFastTrackSourceBag.ManifestId.Value);

                var extracts =
                    _mapper.Map<List<StageArtFastTrackExtract>>(request.ArtFastTrackSourceBag
                        .Extracts);
                if (request.ArtFastTrackSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (request.ArtFastTrackSourceBag.FacilityId.IsNullOrEmpty())
                    {
                        var facs = _facilityRepository.ReadFacilityCache();
                        request.ArtFastTrackSourceBag.SetFacility(facs);
                    }

                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.ArtFastTrackSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any())
                        extracts.ForEach(x => x.Standardize(request.ArtFastTrackSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.ArtFastTrackSourceBag.ManifestId.Value);

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
