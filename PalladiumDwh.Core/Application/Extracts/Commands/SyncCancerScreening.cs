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

    public class SyncCancerScreening : IRequest
    {
        public CancerScreeningSourceBag CancerScreeningSourceBag { get; }

        public SyncCancerScreening(CancerScreeningSourceBag cervicalCancerScreeningSourceBag)
        {
            CancerScreeningSourceBag = cervicalCancerScreeningSourceBag;
        }
    }

    public class SyncCancerScreeningHandler : IRequestHandler<SyncCancerScreening>
    {
        private readonly IMapper _mapper;
        private readonly IStageCancerScreeningExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IMediator _mediator;

        public SyncCancerScreeningHandler(IMapper mapper,
            IStageCancerScreeningExtractRepository stageRepository,
            IFacilityRepository facilityRepository, IMediator mediator)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(SyncCancerScreening request, CancellationToken cancellationToken)
        {
            try
            {
                // await _stageRepository.ClearSite(request.CancerScreeningSourceBag.FacilityId.Value, request.CancerScreeningSourceBag.ManifestId.Value);

                var extracts =
                    _mapper.Map<List<StageCancerScreeningExtract>>(request.CancerScreeningSourceBag
                        .Extracts);
                if (request.CancerScreeningSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (request.CancerScreeningSourceBag.FacilityId.IsNullOrEmpty())
                    {
                        var facs = _facilityRepository.ReadFacilityCache();
                        request.CancerScreeningSourceBag.SetFacility(facs);
                    }

                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.CancerScreeningSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any())
                        extracts.ForEach(x => x.Standardize(request.CancerScreeningSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.CancerScreeningSourceBag.ManifestId.Value);

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
