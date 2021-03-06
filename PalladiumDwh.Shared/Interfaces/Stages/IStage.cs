using System;
using System.Collections.Generic;
using PalladiumDwh.Core.Model.Dto;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Shared.Interfaces.Stages
{
    public interface IStage : IEntity
    {
        int SiteCode { get; set; }
        int PatientPK { get; set; }
        Guid? FacilityId { get; set; }
        Guid? CurrentPatientId { get; set; }
        Guid? LiveSession { get; set; }
        LiveStage LiveStage { get; set; }
        DateTime? Generated { get; set; }
    }
}
