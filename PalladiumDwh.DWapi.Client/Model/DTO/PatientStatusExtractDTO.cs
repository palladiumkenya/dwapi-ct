using System;
using System.Collections.Generic;
using System.Linq;

namespace PalladiumDwh.DWapi.Client.Model.DTO
{
    public class PatientStatusExtractDTO
    {
        public string ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }

        public PatientStatusExtractDTO()
        {
        }

        public PatientStatusExtractDTO(string exitDescription, DateTime? exitDate, string exitReason, string emr, string project, Guid patientId)
        {
            ExitDescription = exitDescription;
            ExitDate = exitDate;
            ExitReason = exitReason;
            Emr = emr;
            Project = project;
            PatientId = patientId;
        }

        public PatientStatusExtractDTO(PatientStatusExtract patientStatusExtract)
        {
            ExitDescription = patientStatusExtract.ExitDescription;
            ExitDate = patientStatusExtract.ExitDate;
            ExitReason = patientStatusExtract.ExitReason;
            Emr = patientStatusExtract.Emr;
            Project = patientStatusExtract.Project;
            PatientId = patientStatusExtract.PatientId;
        }

     

        public IEnumerable<PatientStatusExtractDTO> GeneratePatientStatusExtractDtOs(IEnumerable<PatientStatusExtract> extracts)
        {
            var statusExtractDtos = new List<PatientStatusExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new PatientStatusExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public PatientStatusExtract GeneratePatientStatusExtract(Guid patientId)
        {
            PatientId = patientId;
            return new PatientStatusExtract(ExitDescription, ExitDate, ExitReason, Emr, Project, PatientId);
        }
    }
}
