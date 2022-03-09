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

        public SyncAllergiesChronicIllnessHandler(IMapper mapper,
            IStageAllergiesChronicIllnessExtractRepository stageRepository,
            IFacilityRepository facilityRepository)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<Unit> Handle(SyncAllergiesChronicIllness request, CancellationToken cancellationToken)
        {
            try
            {
                await _stageRepository.ClearSite(request.AllergiesChronicIllnessSourceBag.FacilityId.Value, request.AllergiesChronicIllnessSourceBag.ManifestId.Value);

                var extracts =
                    _mapper.Map<List<StageAllergiesChronicIllnessExtract>>(request.AllergiesChronicIllnessSourceBag
                        .Extracts);
                if (request.AllergiesChronicIllnessSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.AllergiesChronicIllnessSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any())
                        extracts.ForEach(x => x.Standardize(request.AllergiesChronicIllnessSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.AllergiesChronicIllnessSourceBag.ManifestId.Value);
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
