using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Application.Extracts.Source;
using PalladiumDwh.Core.Model.Dto;
using PalladiumDwh.Shared.Interfaces.Stages;

namespace PalladiumDwh.Core.Application.Extracts.Stage
{
    public class StageAdverseEventExtract:StageExtract, IStageAdverseEventExtract
    {
        public string AdverseEvent { get; set; }
        public DateTime? AdverseEventStartDate { get; set; }
        public DateTime? AdverseEventEndDate { get; set; }
        public string Severity { get; set; }
        public string AdverseEventClinicalOutcome { get; set; }
        public string AdverseEventActionTaken { get; set; }
        public bool? AdverseEventIsPregnant { get; set; }
        public DateTime? VisitDate { get; set; }
        public string AdverseEventRegimen { get; set; }
        public string AdverseEventCause { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }

        public  void Standardize(AdverseEventSourceBag sourceBag)
        {
            CheckId();
            LiveSession = sourceBag.ManifestId;
            FacilityId = sourceBag.FacilityId.Value;
        }

        public  void Standardize(AdverseEventSourceBag sourceBag, List<FacilityCacheDto> facilityCacheDtos)
        {
            CheckId();
            LiveSession = sourceBag.ManifestId;

            var fac = facilityCacheDtos.FirstOrDefault(x => x.Code == SiteCode);
            FacilityId = null != fac ? fac.Id : sourceBag.FacilityId.Value;
        }
    }
}
