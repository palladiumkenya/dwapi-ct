using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Application.Extracts.Source;
using PalladiumDwh.Core.Model.Dto;
using PalladiumDwh.Shared.Interfaces.Stages;

namespace PalladiumDwh.Core.Application.Extracts.Stage
{
    public class StageCervicalCancerScreeningExtract:StageExtract, IStageCervicalCancerScreeningExtract
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string VisitType { get; set; }
        public string ScreeningMethod { get; set; }
        public string TreatmentToday { get; set; }
        public string ReferredOut { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public string ScreeningType { get; set; }
        public string ScreeningResult { get; set; }
        public string PostTreatmentComplicationCause { get; set; }
        public string OtherPostTreatmentComplication { get; set; }
        public string ReferralReason { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }


        public  void Standardize(CervicalCancerScreeningSourceBag sourceBag)
        {
            CheckId();
            LiveSession = sourceBag.ManifestId;
            FacilityId = sourceBag.FacilityId.Value;
        }

        public  void Standardize(CervicalCancerScreeningSourceBag sourceBag, List<FacilityCacheDto> facilityCacheDtos)
        {
            CheckId();
            LiveSession = sourceBag.ManifestId;

            var fac = facilityCacheDtos.FirstOrDefault(x => x.Code == SiteCode);
            FacilityId = null != fac ? fac.Id : sourceBag.FacilityId.Value;
        }

        
    }
}
