using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class GbvScreeningExtractDTO : IGbvScreeningExtractDTO
    {
        // public string ExitDescription { get; set; }
        // public DateTime? ExitDate { get; set; }
        // public string ExitReason { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public GbvScreeningExtractDTO()
        {
        }

        public GbvScreeningExtractDTO(string exitDescription, DateTime? exitDate, string exitReason, string emr, string project, Guid patientId)
        {
            // ExitDescription = exitDescription;
            // ExitDate = exitDate;
            // ExitReason = exitReason;
            Emr = emr;
            Project = project;
            PatientId = patientId;
        }

        public GbvScreeningExtractDTO(GbvScreeningExtract GbvScreeningExtract)
        {
            // ExitDescription = GbvScreeningExtract.ExitDescription;
            // ExitDate = GbvScreeningExtract.ExitDate;
            // ExitReason = GbvScreeningExtract.ExitReason;
            Emr = GbvScreeningExtract.Emr;
            Project = GbvScreeningExtract.Project;
            PatientId = GbvScreeningExtract.PatientId;
        }



        public IEnumerable<GbvScreeningExtractDTO> GenerateGbvScreeningExtractDtOs(IEnumerable<GbvScreeningExtract> extracts)
        {
            var statusExtractDtos = new List<GbvScreeningExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new GbvScreeningExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public GbvScreeningExtract GenerateGbvScreeningExtract(Guid patientId)
        {
            PatientId = patientId;
            // return new GbvScreeningExtract(ExitDescription, ExitDate, ExitReason, PatientId, Emr, Project);
            return new GbvScreeningExtract();
        }


        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string IPV { get; set; }
        public string PhysicalIPV { get; set; }
        public string EmotionalIPV { get; set; }
        public string SexualIPV { get; set; }
        public string IPVRelationship { get; set; }
    }
}
