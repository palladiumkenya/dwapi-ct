using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Application.Stage.Repositories
{
    public interface IStageVisitExtractRepository : IStageExtractRepository<StageVisitExtract, PatientVisitExtract>
    {
    }
}
