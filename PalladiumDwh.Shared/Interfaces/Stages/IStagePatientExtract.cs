using System;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public interface IStagePatientExtract : IEntity, IPatientExtract
    {
        Guid? CurrentPatientId { get; set; }
        Guid? LiveSession { get; set; }
        LiveStage LiveStage { get; set; }
    }
}
