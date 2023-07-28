using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class DepressionScreeningExtractDTO : IDepressionScreeningExtractDTO
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
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string PatientUUID { get; set; }

        public DepressionScreeningExtractDTO()
        {
        }

        public DepressionScreeningExtractDTO(DepressionScreeningExtract DepressionScreeningExtract)
        {
            FacilityName=DepressionScreeningExtract.FacilityName;
            VisitID=DepressionScreeningExtract.VisitID;
            VisitDate=DepressionScreeningExtract.VisitDate;
            PHQ9_1=DepressionScreeningExtract.PHQ9_1;
            PHQ9_2=DepressionScreeningExtract.PHQ9_2;
            PHQ9_3=DepressionScreeningExtract.PHQ9_3;
            PHQ9_4=DepressionScreeningExtract.PHQ9_4;
            PHQ9_5=DepressionScreeningExtract.PHQ9_5;
            PHQ9_6=DepressionScreeningExtract.PHQ9_6;
            PHQ9_7=DepressionScreeningExtract.PHQ9_7;
            PHQ9_8=DepressionScreeningExtract.PHQ9_8;
            PHQ9_9=DepressionScreeningExtract.PHQ9_9;
            PHQ_9_rating=DepressionScreeningExtract.PHQ_9_rating;
            DepressionAssesmentScore=DepressionScreeningExtract.DepressionAssesmentScore;

            Emr = DepressionScreeningExtract.Emr;
            Project = DepressionScreeningExtract.Project;
            PatientId = DepressionScreeningExtract.PatientId;
            Date_Created=DepressionScreeningExtract.Date_Created;
            Date_Last_Modified=DepressionScreeningExtract.Date_Last_Modified;
            PatientUUID=DepressionScreeningExtract.PatientUUID;

        }



        public IEnumerable<DepressionScreeningExtractDTO> GenerateDepressionScreeningExtractDtOs(IEnumerable<DepressionScreeningExtract> extracts)
        {
            var statusExtractDtos = new List<DepressionScreeningExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new DepressionScreeningExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public DepressionScreeningExtract GenerateDepressionScreeningExtract(Guid patientId)
        {
            PatientId = patientId;
            return new DepressionScreeningExtract(
                FacilityName,
                VisitID,
                VisitDate,
                PHQ9_1,
                PHQ9_2,
                PHQ9_3,
                PHQ9_4,
                PHQ9_5,
                PHQ9_6,
                PHQ9_7,
                PHQ9_8,
                PHQ9_9,
                PHQ_9_rating,
                DepressionAssesmentScore,
                PatientId,
                Emr,
                Project,
                Date_Created,
                Date_Last_Modified,
                PatientUUID
                );
        }
    }
}
