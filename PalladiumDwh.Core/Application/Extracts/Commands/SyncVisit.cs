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
    public class SyncVisit : IRequest
    {
        public VisitSourceBag VisitSourceBag { get; }

        public SyncVisit(VisitSourceBag visitSourceBag)
        {
            VisitSourceBag = visitSourceBag;
        }
    }

    public class SyncVisitHandler : IRequestHandler<SyncVisit>
    {
        private readonly IMapper _mapper;
        private readonly IStageVisitExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;

        public SyncVisitHandler(IMapper mapper, IStageVisitExtractRepository stageRepository,
            IFacilityRepository facilityRepository)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<Unit> Handle(SyncVisit request, CancellationToken cancellationToken)
        {
            try
            {
        //        await _stageRepository.ClearSite(request.VisitSourceBag.FacilityId.Value, request.VisitSourceBag.ManifestId.Value);
                // standardize

                var extracts = _mapper.Map<List<StageVisitExtract>>(request.VisitSourceBag.Extracts);

                if (request.VisitSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (request.VisitSourceBag.FacilityId.IsNullOrEmpty())
                    {
                        var facs = _facilityRepository.ReadFacilityCache();
                        request.VisitSourceBag.SetFacility(facs);
                    }

                    if (extracts.Any())
                        extracts.ForEach(x => x.Standardize(request.VisitSourceBag));
                }
                else
                {

                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any())
                        extracts.ForEach(x => x.Standardize(request.VisitSourceBag, facs));
                }

                //stage
                await _stageRepository.SyncStage(extracts, request.VisitSourceBag.ManifestId.Value);

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
