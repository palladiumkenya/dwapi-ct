using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Application.Extracts.Stage.Repositories
{
    public interface IStageVisitExtractRepository : IStageExtractRepository<StageVisitExtract, PatientVisitExtract>
    {
    }
}
