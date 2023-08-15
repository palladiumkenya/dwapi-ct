using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Application.Extracts.Source;
using PalladiumDwh.Core.Model.Dto;
using PalladiumDwh.Shared.Interfaces.Stages;

namespace PalladiumDwh.Core.Application.Extracts.Stage
{
    public class StageIITRiskScoresExtract:StageExtract, IStageIITRiskScoresExtract
    {
        public string FacilityName { get; set; }
        public string SourceSysUUID { get; set; }

        public decimal? RiskScore  { get; set; }
        public string RiskFactors  { get; set; }
        public string RiskDescription  { get; set; }
        public DateTime? RiskEvaluationDate  { get; set; }
        public DateTime? Date_Created  { get; set; }
        public DateTime? Date_Last_Modified  { get; set; }


        public  void Standardize(IITRiskScoresSourceBag sourceBag)
        {
            CheckId();
            LiveSession = sourceBag.ManifestId;
            FacilityId = sourceBag.FacilityId.Value;
        }

        public  void Standardize(IITRiskScoresSourceBag sourceBag, List<FacilityCacheDto> facilityCacheDtos)
        {
            CheckId();
            LiveSession = sourceBag.ManifestId;

            var fac = facilityCacheDtos.FirstOrDefault(x => x.Code == SiteCode);
            FacilityId = null != fac ? fac.Id : sourceBag.FacilityId.Value;
        }

        
    }
}
