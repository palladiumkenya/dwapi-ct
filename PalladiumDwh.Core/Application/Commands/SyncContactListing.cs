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
    public class SyncContactListing : IRequest
    {
        public ContactListingSourceBag ContactListingSourceBag { get; }

        public SyncContactListing(ContactListingSourceBag contactListingSourceBag)
        {
            ContactListingSourceBag = contactListingSourceBag;
        }
    }

    public class SyncContactListingHandler : IRequestHandler<SyncContactListing>
    {
        private readonly IMapper _mapper;
        private readonly IStageContactListingExtractRepository _stageRepository;
        private readonly IFacilityRepository _facilityRepository;

        public SyncContactListingHandler(IMapper mapper, IStageContactListingExtractRepository stageRepository,
            IFacilityRepository facilityRepository)
        {
            _mapper = mapper;
            _stageRepository = stageRepository;
            _facilityRepository = facilityRepository;
        }

        public async Task<Unit> Handle(SyncContactListing request, CancellationToken cancellationToken)
        {
            try
            {
                await _stageRepository.ClearSite(request.ContactListingSourceBag.FacilityId.Value, request.ContactListingSourceBag.ManifestId.Value);

                var extracts = _mapper.Map<List<StageContactListingExtract>>(request.ContactListingSourceBag.Extracts);
                if (request.ContactListingSourceBag.EmrSetup == EmrSetup.SingleFacility)
                {
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.ContactListingSourceBag));
                }
                else
                {
                    var facs = _facilityRepository.ReadFacilityCache();
                    if (extracts.Any()) extracts.ForEach(x => x.Standardize(request.ContactListingSourceBag, facs));
                }

                await _stageRepository.SyncStage(extracts, request.ContactListingSourceBag.ManifestId.Value);
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
