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

    public class SyncAllergiesChronicIllness : IRequest
    {
        public AllergiesChronicIllnessSourceBag AllergiesChronicIllnessSourceBag { get; }

        public SyncAllergiesChronicIllness(AllergiesChronicIllnessSourceBag allergiesChronicIllnessSourceBag)
        {
            AllergiesChronicIllnessSourceBag = allergiesChronicIllnessSourceBag;
        }
    }

    public class SyncAllergiesChronicIllnessHandler : IRequestHandler<SyncAllergiesChronicIllness>
    {
        private readonly IMapper _mapper;
        private readonly IStageAllergiesChronicIllnessExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IMediator _mediator;

        public SyncAllergiesChronicIllnessHandler(IMapper mapper,
            IStageAllergiesChronicIllnessExtractRepository stageRepository,
            IFacilityRepository facilityRepository, IMediator mediator)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(SyncAllergiesChronicIllness request, CancellationToken cancellationToken)
        {
            try
            {
                // await _stageRepository.ClearSite(request.AllergiesChronicIllnessSourceBag.FacilityId.Value, request.AllergiesChronicIllnessSourceBag.ManifestId.Value);

                var extracts =
                    _mapper.Map<List<StageAllergiesChronicIllnessExtract>>(request.AllergiesChronicIllnessSourceBag
                        .Extracts);
                if (request.AllergiesChronicIllnessSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (request.AllergiesChronicIllnessSourceBag.FacilityId.IsNullOrEmpty())
                    {
                        var facs = _facilityRepository.ReadFacilityCache();
                        request.AllergiesChronicIllnessSourceBag.SetFacility(facs);
                    }

                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.AllergiesChronicIllnessSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any())
                        extracts.ForEach(x => x.Standardize(request.AllergiesChronicIllnessSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.AllergiesChronicIllnessSourceBag.ManifestId.Value);

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
