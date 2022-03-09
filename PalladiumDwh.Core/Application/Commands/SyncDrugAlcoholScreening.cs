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

    public class SyncDrugAlcoholScreening : IRequest
    {
        public DrugAlcoholScreeningSourceBag DrugAlcoholScreeningSourceBag { get; }

        public SyncDrugAlcoholScreening(DrugAlcoholScreeningSourceBag drugAlcoholScreeningSourceBag)
        {
            DrugAlcoholScreeningSourceBag = drugAlcoholScreeningSourceBag;
        }
    }

    public class SyncDrugAlcoholScreeningHandler : IRequestHandler<SyncDrugAlcoholScreening>
    {
        private readonly IMapper _mapper;
        private readonly IStageDrugAlcoholScreeningExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;

        public SyncDrugAlcoholScreeningHandler(IMapper mapper,
            IStageDrugAlcoholScreeningExtractRepository stageRepository,
            IFacilityRepository facilityRepository)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<Unit> Handle(SyncDrugAlcoholScreening request, CancellationToken cancellationToken)
        {
            try
            {
                await _stageRepository.ClearSite(request.DrugAlcoholScreeningSourceBag.FacilityId.Value, request.DrugAlcoholScreeningSourceBag.ManifestId.Value);

                var extracts =
                    _mapper.Map<List<StageDrugAlcoholScreeningExtract>>(request.DrugAlcoholScreeningSourceBag.Extracts);
                if (request.DrugAlcoholScreeningSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.DrugAlcoholScreeningSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any())
                        extracts.ForEach(x => x.Standardize(request.DrugAlcoholScreeningSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.DrugAlcoholScreeningSourceBag.ManifestId.Value);
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
