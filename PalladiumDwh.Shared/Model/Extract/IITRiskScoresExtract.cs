using System;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
{
    public class IITRiskScoresExtract : Entity, IIITRiskScoresExtract
    {
        public string FacilityName { get; set; }
        public Guid PatientId { get; set; }

        public string SourceSysUUID { get; set; }

        public decimal? RiskScore  { get; set; }
        public string RiskFactors  { get; set; }
        public string RiskDescription  { get; set; }
        public DateTime? RiskEvaluationDate  { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }

        public IITRiskScoresExtract()
        {
            Created = DateTime.Now;
        }

        public IITRiskScoresExtract(string facilityName,  Guid patientId, string emr, string project,  string sourceSysUUID, decimal? riskScore , string riskFactors , string riskDescription , DateTime? riskEvaluationDate ,DateTime? date_Created,DateTime? date_Last_Modified)
        {
            FacilityName = facilityName;
            SourceSysUUID = sourceSysUUID;
            RiskEvaluationDate = riskEvaluationDate;
            RiskScore = riskScore;
            RiskFactors = riskFactors;
            RiskDescription = riskDescription;
            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
            this.StandardizeExtract();
        }

    }
}
