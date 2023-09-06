using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Application.Extracts.Source;
using PalladiumDwh.Core.Model.Dto;
using PalladiumDwh.Shared.Interfaces.Stages;

namespace PalladiumDwh.Core.Application.Extracts.Stage
{
    public class StageDepressionScreeningExtract:StageExtract, IStageDepressionScreeningExtract
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string PHQ9_1 { get; set; }
        public string PHQ9_2 { get; set; }
        public string PHQ9_3 { get; set; }
        public string PHQ9_4 { get; set; }
        public string PHQ9_5 { get; set; }
        public string PHQ9_6 { get; set; }
        public string PHQ9_7 { get; set; }
        public string PHQ9_8 { get; set; }
        public string PHQ9_9 { get; set; }
        public string PHQ_9_rating { get; set; }
        public int? DepressionAssesmentScore { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }


        public  void Standardize(DepressionScreeningSourceBag sourceBag)
        {
            CheckId();
            LiveSession = sourceBag.ManifestId;
            FacilityId = sourceBag.FacilityId.Value;
        }

        public  void Standardize(DepressionScreeningSourceBag sourceBag, List<FacilityCacheDto> facilityCacheDtos)
        {
            CheckId();
            LiveSession = sourceBag.ManifestId;

            var fac = facilityCacheDtos.FirstOrDefault(x => x.Code == SiteCode);
            FacilityId = null != fac ? fac.Id : sourceBag.FacilityId.Value;
        }
    }
}
