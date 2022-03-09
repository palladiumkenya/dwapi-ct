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
    public class SyncOtz : IRequest
    {
        public OtzSourceBag OtzSourceBag { get; }

        public SyncOtz(OtzSourceBag otzSourceBag)
        {
            OtzSourceBag = otzSourceBag;
        }
    }

    public class SyncOtzHandler : IRequestHandler<SyncOtz>
    {
        private readonly IMapper _mapper;
        private readonly IStageOtzExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;

        public SyncOtzHandler(IMapper mapper, IStageOtzExtractRepository stageRepository,
            IFacilityRepository facilityRepository)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<Unit> Handle(SyncOtz request, CancellationToken cancellationToken)
        {
            try
            {
                await _stageRepository.ClearSite(request.OtzSourceBag.FacilityId.Value, request.OtzSourceBag.ManifestId.Value);
                var extracts = _mapper.Map<List<StageOtzExtract>>(request.OtzSourceBag.Extracts);
                if (request.OtzSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.OtzSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.OtzSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.OtzSourceBag.ManifestId.Value);
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
