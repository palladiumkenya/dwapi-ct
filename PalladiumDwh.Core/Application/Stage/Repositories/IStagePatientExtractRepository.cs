using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.Core.Model;

namespace PalladiumDwh.Core.Application.Stage.Repositories
{
    public interface IStagePatientExtractRepository
    {
        Task ClearSite(Guid facilityId, Guid manifestId);
        Task SyncStage(List<StagePatientExtract> extracts, Guid manifestId);
    }
}
