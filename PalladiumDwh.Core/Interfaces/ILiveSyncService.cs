using System;
using System.Collections.Generic;
using PalladiumDwh.Core.Exchange;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Interfaces
{
    public interface ILiveSyncService
    {
        void SyncManifest(ManifestDto manifest);
        void SyncStats(IFacilityRepository facilityRepository, List<Guid> facilityId);
    }
}
