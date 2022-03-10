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
    public class SyncCovid : IRequest
    {
        public CovidSourceBag CovidSourceBag { get; }

        public SyncCovid(CovidSourceBag covidSourceBag)
        {
            CovidSourceBag = covidSourceBag;
        }
    }

    public class SyncCovidHandler : IRequestHandler<SyncCovid>
    {
        private readonly IMapper _mapper;
        private readonly IStageCovidExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;

        public SyncCovidHandler(IMapper mapper, IStageCovidExtractRepository stageRepository,
            IFacilityRepository facilityRepository)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<Unit> Handle(SyncCovid request, CancellationToken cancellationToken)
        {
            try
            {
                await _stageRepository.ClearSite(request.CovidSourceBag.FacilityId.Value, request.CovidSourceBag.ManifestId.Value);

                var extracts = _mapper.Map<List<StageCovidExtract>>(request.CovidSourceBag.Extracts);
                if (request.CovidSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (request.CovidSourceBag.FacilityId.IsNullOrEmpty())
                    {
                        var facs = _facilityRepository.ReadFacilityCache();
                        request.CovidSourceBag.SetFacility(facs);
                    }

                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.CovidSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.CovidSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.CovidSourceBag.ManifestId.Value);
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
