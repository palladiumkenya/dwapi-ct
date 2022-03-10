using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PalladiumDwh.Core.Application.Extracts.Stage.Repositories
{
    public interface IStagePatientExtractRepository
    {
        Task ClearSite(Guid facilityId, Guid manifestId);
        Task SyncStage(List<StagePatientExtract> extracts, Guid manifestId);
    }
}
