using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class DepressionScreeningExtractDTO : IDepressionScreeningExtractDTO
    {
        // public string ExitDescription { get; set; }
        // public DateTime? ExitDate { get; set; }
        // public string ExitReason { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public DepressionScreeningExtractDTO()
        {
        }

        public DepressionScreeningExtractDTO(string exitDescription, DateTime? exitDate, string exitReason, string emr, string project, Guid patientId)
        {
            // ExitDescription = exitDescription;
            // ExitDate = exitDate;
            // ExitReason = exitReason;
            Emr = emr;
            Project = project;
            PatientId = patientId;
        }

        public DepressionScreeningExtractDTO(DepressionScreeningExtract DepressionScreeningExtract)
        {
            // ExitDescription = DepressionScreeningExtract.ExitDescription;
            // ExitDate = DepressionScreeningExtract.ExitDate;
            // ExitReason = DepressionScreeningExtract.ExitReason;
            Emr = DepressionScreeningExtract.Emr;
            Project = DepressionScreeningExtract.Project;
            PatientId = DepressionScreeningExtract.PatientId;
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
            // return new DepressionScreeningExtract(ExitDescription, ExitDate, ExitReason, PatientId, Emr, Project);
            return new DepressionScreeningExtract();
        }


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
    }
}
