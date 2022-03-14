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

    public class SyncBaseline : IRequest
    {
        public BaselineSourceBag BaselineSourceBag { get; }

        public SyncBaseline(BaselineSourceBag baselineSourceBag)
        {
            BaselineSourceBag = baselineSourceBag;
        }
    }

    public class SyncBaselineHandler : IRequestHandler<SyncBaseline>
    {
        private readonly IMapper _mapper;
        private readonly IStageBaselineExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;

        public SyncBaselineHandler(IMapper mapper, IStageBaselineExtractRepository stageRepository,
            IFacilityRepository facilityRepository)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<Unit> Handle(SyncBaseline request, CancellationToken cancellationToken)
        {
            try
            {
                //await _stageRepository.ClearSite(request.BaselineSourceBag.FacilityId.Value, request.BaselineSourceBag.ManifestId.Value);

                var extracts = _mapper.Map<List<StageBaselineExtract>>(request.BaselineSourceBag.Extracts);
                if (request.BaselineSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (request.BaselineSourceBag.FacilityId.IsNullOrEmpty())
                    {
                        var facs = _facilityRepository.ReadFacilityCache();
                        request.BaselineSourceBag.SetFacility(facs);
                    }

                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.BaselineSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.BaselineSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.BaselineSourceBag.ManifestId.Value);
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
