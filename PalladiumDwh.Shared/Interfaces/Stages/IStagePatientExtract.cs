using System;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Interfaces.Extracts;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Shared.Interfaces.Stages
{
    public interface IStagePatientExtract : IEntity, IPatientExtract
    {
        int SiteCode { get; set; }
        Guid? CurrentPatientId { get; set; }
        Guid? LiveSession { get; set; }
        LiveStage LiveStage { get; set; }
    }
}
