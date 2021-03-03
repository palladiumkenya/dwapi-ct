using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class OvcExtractDTO : IOvcExtractDTO
    {
        // public string ExitDescription { get; set; }
        // public DateTime? ExitDate { get; set; }
        // public string ExitReason { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public OvcExtractDTO()
        {
        }

        public OvcExtractDTO(string exitDescription, DateTime? exitDate, string exitReason, string emr, string project, Guid patientId)
        {
            // ExitDescription = exitDescription;
            // ExitDate = exitDate;
            // ExitReason = exitReason;
            Emr = emr;
            Project = project;
            PatientId = patientId;
        }

        public OvcExtractDTO(OvcExtract OvcExtract)
        {
            // ExitDescription = OvcExtract.ExitDescription;
            // ExitDate = OvcExtract.ExitDate;
            // ExitReason = OvcExtract.ExitReason;
            Emr = OvcExtract.Emr;
            Project = OvcExtract.Project;
            PatientId = OvcExtract.PatientId;
        }



        public IEnumerable<OvcExtractDTO> GenerateOvcExtractDtOs(IEnumerable<OvcExtract> extracts)
        {
            var statusExtractDtos = new List<OvcExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new OvcExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public OvcExtract GenerateOvcExtract(Guid patientId)
        {
            PatientId = patientId;
            // return new OvcExtract(ExitDescription, ExitDate, ExitReason, PatientId, Emr, Project);
            return new OvcExtract();
        }


        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? OVCEnrollmentDate { get; set; }
        public string RelationshipToClient { get; set; }
        public string EnrolledinCPIMS { get; set; }
        public string CPIMSUniqueIdentifier { get; set; }
        public string PartnerOfferingOVCServices { get; set; }
        public string OVCExitReason { get; set; }
        public DateTime? ExitDate { get; set; }
    }
}
