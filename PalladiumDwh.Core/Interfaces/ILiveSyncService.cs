using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PalladiumDwh.Core.Exchange;

namespace PalladiumDwh.Core.Interfaces
{
    public interface ILiveSyncService
    {
        Task<Result> SyncManifest(ManifestDto manifest);
        Task<Result> SyncStats(IFacilityRepository facilityRepository, List<Guid> facilityId);
        Task<Result> SyncMetrics(List<MetricDto> metrics);
    }
}
