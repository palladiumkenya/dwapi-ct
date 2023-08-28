using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Core.Application.Extracts.Source.Dto
{
    public class IITRiskScoresSourceDto:SourceDto, IIITRiskScores
    {
        public string FacilityName { get; set; }
        public string SourceSysUUID { get; set; }

        public string RiskScore  { get; set; }
        public string RiskFactors  { get; set; }
        public string RiskDescription  { get; set; }
        public DateTime? RiskEvaluationDate  { get; set; }
        public DateTime? Date_Created  { get; set; }
        public DateTime? Date_Last_Modified  { get; set; }

    }
}