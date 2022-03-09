using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Application.Source;
using PalladiumDwh.Core.Model.Dto;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Interfaces.Stages;

namespace PalladiumDwh.Core.Model
{
    public abstract class StageExtract:IStage
    {
        public Guid Id { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public bool Voided { get; set; }
        public bool Processed { get; set; }
        public int SiteCode { get; set; }
        public int PatientPK { get; set; }
        public Guid? FacilityId { get; set; }
        public Guid? CurrentPatientId { get; set; }
        public Guid? LiveSession { get; set; }
        public LiveStage LiveStage { get; set; }
    }
}
