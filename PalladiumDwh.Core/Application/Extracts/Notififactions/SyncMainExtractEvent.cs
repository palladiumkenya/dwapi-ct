using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PalladiumDwh.Core.Interfaces;

namespace PalladiumDwh.Core.Application.Extracts.Notififactions
{
    public class SyncMainExtractEvent:INotification
    {
       public List<Guid> FacilityIds { get; }

       public SyncMainExtractEvent(List<Guid> facilityIds)
       {
           FacilityIds = facilityIds;
       }
    }

    public class SyncMainExtractEventHandler : INotificationHandler<SyncMainExtractEvent>
    {
        private readonly ILiveSyncService _liveSyncService;
        private readonly IFacilityRepository _facilityRepository;

        public SyncMainExtractEventHandler(ILiveSyncService liveSync, IFacilityRepository facilityRepository)
        {
            _liveSyncService = liveSync;
            _facilityRepository = facilityRepository;
        }

        public async Task Handle(SyncMainExtractEvent notification, CancellationToken cancellationToken)
        {
            await _liveSyncService.SyncStats(_facilityRepository, notification.FacilityIds);
        }
    }
}