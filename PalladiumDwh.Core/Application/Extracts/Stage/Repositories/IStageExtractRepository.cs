using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.Shared.Interfaces.Stages;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Application.Extracts.Stage.Repositories
{
    public interface IStageExtractRepository<T,D>
        where T :IStage
        where D :IEntity
    {
        Task ClearSite(Guid facilityId, Guid manifestId);
        Task SyncStage(List<T> extracts, Guid manifestId);
    }
}
