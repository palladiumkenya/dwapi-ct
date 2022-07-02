using System;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Extentions;
using PalladiumDwh.Shared.Interfaces.Stages;

namespace PalladiumDwh.Core.Application.Extracts.Stage
{
    public abstract class StageExtract : IStage
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
        public DateTime? Generated { get; set; } = DateTime.Now;

        protected virtual void CheckId()
        {
            Id = LiveGuid.NewGuid();
            //Id = Id.IsNullOrEmpty() ? Guid.NewGuid() : Id;
            this.StandardizeExtract();
        }
    }
}
