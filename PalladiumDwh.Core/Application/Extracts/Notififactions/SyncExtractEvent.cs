using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PalladiumDwh.Core.Interfaces;

namespace PalladiumDwh.Core.Application.Extracts.Notififactions
{
    public class SyncExtractEvent:INotification
    {
       public List<Guid?> FacilityIds { get; }

       public SyncExtractEvent(List<Guid?> facilityIds)
       {
           FacilityIds = facilityIds;
       }
    }

    public class SyncExtractEventHandler : INotificationHandler<SyncExtractEvent>
    {
        private readonly ILiveSyncService _liveSyncService;
        private readonly IFacilityRepository _facilityRepository;

        public SyncExtractEventHandler(ILiveSyncService liveSync, IFacilityRepository facilityRepository)
        {
            _liveSyncService = liveSync;
            _facilityRepository = facilityRepository;
        }

        public async Task Handle(SyncExtractEvent notification, CancellationToken cancellationToken)
        {
            var ids = new List<Guid>();
            foreach (var id in notification.FacilityIds)
            {
                if (id.HasValue)
                    ids.Add(id.Value);
            }

            if (ids.Any())
                await _liveSyncService.SyncStats(_facilityRepository, ids);
        }

    }
}