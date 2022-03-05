using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Core.Interfaces.Sync
{
    public interface IStagePatientExtractRepository
    {
        Task ClearSite(Guid facilityId, Guid sessionId);
        Task Stage(List<StagePatientExtract> extracts, Guid session);
    }
}
