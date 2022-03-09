using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Application.Stage.Repositories
{
    public interface IStageArtExtractRepository : IStageExtractRepository<StageArtExtract, PatientArtExtract>{}
}