using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Model.Bag;
using PalladiumDwh.Core.Model.Dto;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Interfaces.Stages;

namespace PalladiumDwh.Core.Model
{
    public class StageExtract:IStage
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


        public void Standardize(VisitSourceBag sourceBag)
        {
            LiveSession = sourceBag.ManifestId;
            FacilityId = sourceBag.FacilityId.Value;
        }

        public void Standardize(VisitSourceBag sourceBag, List<FacilityCacheDto> facilityCacheDtos)
        {
            LiveSession = sourceBag.ManifestId;

            var fac = facilityCacheDtos.FirstOrDefault(x => x.Code == SiteCode);
            FacilityId = null != fac ? fac.Id : sourceBag.FacilityId.Value;
        }
    }
}