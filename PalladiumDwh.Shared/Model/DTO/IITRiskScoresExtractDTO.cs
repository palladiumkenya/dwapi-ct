using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class IITRiskScoresExtractDTO : IIITRiskScoresExtractDTO
    {
        public Guid PatientId { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public string FacilityName { get; set; }
        public string SourceSysUUID { get; set; }

        public decimal? RiskScore  { get; set; }
        public string RiskFactors  { get; set; }
        public string RiskDescription  { get; set; }
        public DateTime? RiskEvaluationDate  { get; set; }
        public DateTime? Date_Created  { get; set; }
        public DateTime? Date_Last_Modified  { get; set; }

        
        public IITRiskScoresExtractDTO()
        {
        }

        public IITRiskScoresExtractDTO(IITRiskScoresExtract IITRiskScoresExtract)
        {
            FacilityName=IITRiskScoresExtract.FacilityName;
            SourceSysUUID = IITRiskScoresExtract.SourceSysUUID;
            RiskEvaluationDate = IITRiskScoresExtract.RiskEvaluationDate;
            RiskScore = IITRiskScoresExtract.RiskScore;
            RiskFactors = IITRiskScoresExtract.RiskFactors;
            RiskDescription = IITRiskScoresExtract.RiskDescription;
            Emr=IITRiskScoresExtract.Emr;
            Project=IITRiskScoresExtract.Project;
            PatientId=IITRiskScoresExtract.PatientId;
            Date_Created=IITRiskScoresExtract.Date_Created;
            Date_Last_Modified=IITRiskScoresExtract.Date_Last_Modified;


        }

        public IEnumerable<IITRiskScoresExtractDTO> GenerateIITRiskScoresExtractDtOs(IEnumerable<IITRiskScoresExtract> extracts)
        {
            var statusExtractDtos = new List<IITRiskScoresExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new IITRiskScoresExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public IITRiskScoresExtract GenerateIITRiskScoresExtract(Guid patientId)
        {
            PatientId = patientId;
            return new IITRiskScoresExtract(
                FacilityName,
                PatientId,
                Emr,
                Project,
                SourceSysUUID,
                RiskScore,
                RiskFactors,
                RiskDescription,
                RiskEvaluationDate,
                Date_Created,
                Date_Last_Modified

            );
        }

    }
}
