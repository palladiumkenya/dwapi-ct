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

    public class SyncLaboratory : IRequest
    {
        public LaboratorySourceBag LaboratorySourceBag { get; }

        public SyncLaboratory(LaboratorySourceBag laboratorySourceBag)
        {
            LaboratorySourceBag = laboratorySourceBag;
        }
    }

    public class SyncLaboratoryHandler : IRequestHandler<SyncLaboratory>
    {
        private readonly IMapper _mapper;
        private readonly IStageLaboratoryExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;

        public SyncLaboratoryHandler(IMapper mapper, IStageLaboratoryExtractRepository stageRepository,
            IFacilityRepository facilityRepository)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<Unit> Handle(SyncLaboratory request, CancellationToken cancellationToken)
        {
            try
            {
                await _stageRepository.ClearSite(request.LaboratorySourceBag.FacilityId.Value, request.LaboratorySourceBag.ManifestId.Value);

                var extracts = _mapper.Map<List<StageLaboratoryExtract>>(request.LaboratorySourceBag.Extracts);
                if (request.LaboratorySourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.LaboratorySourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.LaboratorySourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.LaboratorySourceBag.ManifestId.Value);
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
