using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.Shared.Interfaces.Extracts;
using PalladiumDwh.Shared.Interfaces.Stages;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Application.Stage.Repositories
{
    public interface IStageExtractRepository<T,D>
        where T :IStage
        where D :IEntity
    {
        Task ClearSite(Guid facilityId, Guid manifestId);
        Task SyncStage(List<T> extracts, Guid manifestId);
    }
}
