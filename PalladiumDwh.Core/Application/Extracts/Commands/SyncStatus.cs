using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using PalladiumDwh.Core.Application.Extracts.Source;
using PalladiumDwh.Core.Application.Extracts.Stage;
using PalladiumDwh.Core.Application.Extracts.Stage.Repositories;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Extentions;

namespace PalladiumDwh.Core.Application.Extracts.Commands
{
    public class SyncStatus : IRequest
    {
        public StatusSourceBag StatusSourceBag { get; }

        public SyncStatus(StatusSourceBag statusSourceBag)
        {
            StatusSourceBag = statusSourceBag;
        }
    }

    public class SyncStatusHandler : IRequestHandler<SyncStatus>
    {
        private readonly IMapper _mapper;
        private readonly IStageStatusExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;

        public SyncStatusHandler(IMapper mapper, IStageStatusExtractRepository stageRepository,
            IFacilityRepository facilityRepository)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<Unit> Handle(SyncStatus request, CancellationToken cancellationToken)
        {
            try
            {

              //  await _stageRepository.ClearSite(request.StatusSourceBag.FacilityId.Value, request.StatusSourceBag.ManifestId.Value);

                var extracts = _mapper.Map<List<StageStatusExtract>>(request.StatusSourceBag.Extracts);
                if (request.StatusSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (request.StatusSourceBag.FacilityId.IsNullOrEmpty())
                    {
                        var facs = _facilityRepository.ReadFacilityCache();
                        request.StatusSourceBag.SetFacility(facs);
                    }

                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.StatusSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.StatusSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.StatusSourceBag.ManifestId.Value);
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
