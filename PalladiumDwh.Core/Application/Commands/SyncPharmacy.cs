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

    public class SyncPharmacy : IRequest
    {
        public PharmacySourceBag PharmacySourceBag { get; }

        public SyncPharmacy(PharmacySourceBag pharmacySourceBag)
        {
            PharmacySourceBag = pharmacySourceBag;
        }
    }

    public class SyncPharmacyHandler : IRequestHandler<SyncPharmacy>
    {
        private readonly IMapper _mapper;
        private readonly IStagePharmacyExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;

        public SyncPharmacyHandler(IMapper mapper, IStagePharmacyExtractRepository stageRepository,
            IFacilityRepository facilityRepository)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<Unit> Handle(SyncPharmacy request, CancellationToken cancellationToken)
        {
            try
            {
                await _stageRepository.ClearSite(request.PharmacySourceBag.FacilityId.Value, request.PharmacySourceBag.ManifestId.Value);

                var extracts = _mapper.Map<List<StagePharmacyExtract>>(request.PharmacySourceBag.Extracts);
                if (request.PharmacySourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.PharmacySourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.PharmacySourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.PharmacySourceBag.ManifestId.Value);
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
