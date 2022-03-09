using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Application.Source;
using PalladiumDwh.Core.Model.Dto;
using PalladiumDwh.Shared.Interfaces.Stages;

namespace PalladiumDwh.Core.Model
{
    public class StageStatusExtract:StageExtract, IStageStatusExtract
    {
        public string TOVerified { get; set; }
        public DateTime? TOVerifiedDate { get; set; }
        public DateTime? ReEnrollmentDate { get; set; }
        public string ReasonForDeath { get; set; }
        public string SpecificDeathReason { get; set; }
        public DateTime? DeathDate { get; set; }
        public string ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }

        public  void Standardize(StatusSourceBag sourceBag)
        {
            LiveSession = sourceBag.ManifestId;
            FacilityId = sourceBag.FacilityId.Value;
        }

        public  void Standardize(StatusSourceBag sourceBag, List<FacilityCacheDto> facilityCacheDtos)
        {
            LiveSession = sourceBag.ManifestId;

            var fac = facilityCacheDtos.FirstOrDefault(x => x.Code == SiteCode);
            FacilityId = null != fac ? fac.Id : sourceBag.FacilityId.Value;
        }
    }
}
