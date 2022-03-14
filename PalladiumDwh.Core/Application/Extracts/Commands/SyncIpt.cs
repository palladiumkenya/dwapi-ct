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

    public class SyncIpt : IRequest
    {
        public IptSourceBag IptSourceBag { get; }

        public SyncIpt(IptSourceBag iptSourceBag)
        {
            IptSourceBag = iptSourceBag;
        }
    }

    public class SyncIptHandler : IRequestHandler<SyncIpt>
    {
        private readonly IMapper _mapper;
        private readonly IStageIptExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;

        public SyncIptHandler(IMapper mapper, IStageIptExtractRepository stageRepository,
            IFacilityRepository facilityRepository)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<Unit> Handle(SyncIpt request, CancellationToken cancellationToken)
        {
            try
            {
               // await _stageRepository.ClearSite(request.IptSourceBag.FacilityId.Value, request.IptSourceBag.ManifestId.Value);

                var extracts = _mapper.Map<List<StageIptExtract>>(request.IptSourceBag.Extracts);
                if (request.IptSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (request.IptSourceBag.FacilityId.IsNullOrEmpty())
                    {

                        var facs = _facilityRepository.ReadFacilityCache();
                        request.IptSourceBag.SetFacility(facs);
                    }

                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.IptSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.IptSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.IptSourceBag.ManifestId.Value);
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
