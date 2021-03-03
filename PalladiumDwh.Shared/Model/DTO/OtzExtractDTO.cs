using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class OtzExtractDTO : IOtzExtractDTO
    {
        // public string ExitDescription { get; set; }
        // public DateTime? ExitDate { get; set; }
        // public string ExitReason { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public OtzExtractDTO()
        {
        }

        public OtzExtractDTO(string exitDescription, DateTime? exitDate, string exitReason, string emr, string project, Guid patientId)
        {
            // ExitDescription = exitDescription;
            // ExitDate = exitDate;
            // ExitReason = exitReason;
            Emr = emr;
            Project = project;
            PatientId = patientId;
        }

        public OtzExtractDTO(OtzExtract OtzExtract)
        {
            // ExitDescription = OtzExtract.ExitDescription;
            // ExitDate = OtzExtract.ExitDate;
            // ExitReason = OtzExtract.ExitReason;
            Emr = OtzExtract.Emr;
            Project = OtzExtract.Project;
            PatientId = OtzExtract.PatientId;
        }



        public IEnumerable<OtzExtractDTO> GenerateOtzExtractDtOs(IEnumerable<OtzExtract> extracts)
        {
            var statusExtractDtos = new List<OtzExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new OtzExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public OtzExtract GenerateOtzExtract(Guid patientId)
        {
            PatientId = patientId;
            // return new OtzExtract(ExitDescription, ExitDate, ExitReason, PatientId, Emr, Project);
            return new OtzExtract();
        }


        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? OTZEnrollmentDate { get; set; }
        public string TransferInStatus { get; set; }
        public string ModulesPreviouslyCovered { get; set; }
        public string ModulesCompletedToday { get; set; }
        public string SupportGroupInvolvement { get; set; }
        public string Remarks { get; set; }
        public string TransitionAttritionReason { get; set; }
        public DateTime? OutcomeDate { get; set; }
    }
}
