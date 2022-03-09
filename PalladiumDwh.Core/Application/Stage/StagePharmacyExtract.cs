using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Application.Source;
using PalladiumDwh.Core.Model.Dto;
using PalladiumDwh.Shared.Interfaces.Stages;

namespace PalladiumDwh.Core.Model
{
    public class StagePharmacyExtract:StageExtract, IStagePharmacyExtract
    {
        public string RegimenChangedSwitched { get; set; }
        public string RegimenChangeSwitchReason { get; set; }
        public string StopRegimenReason { get; set; }
        public DateTime? StopRegimenDate { get; set; }
        public int? VisitID { get; set; }
        public string Drug { get; set; }
        public string Provider { get; set; }
        public DateTime? DispenseDate { get; set; }
        public decimal? Duration { get; set; }
        public DateTime? ExpectedReturn { get; set; }
        public string TreatmentType { get; set; }
        public string RegimenLine { get; set; }
        public string PeriodTaken { get; set; }
        public string ProphylaxisType { get; set; }

        public  void Standardize(PharmacySourceBag sourceBag)
        {
            LiveSession = sourceBag.ManifestId;
            FacilityId = sourceBag.FacilityId.Value;
        }

        public  void Standardize(PharmacySourceBag sourceBag, List<FacilityCacheDto> facilityCacheDtos)
        {
            LiveSession = sourceBag.ManifestId;

            var fac = facilityCacheDtos.FirstOrDefault(x => x.Code == SiteCode);
            FacilityId = null != fac ? fac.Id : sourceBag.FacilityId.Value;
        }
    }
}
