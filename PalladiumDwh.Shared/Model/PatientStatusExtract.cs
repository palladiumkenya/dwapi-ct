using System;

namespace PalladiumDwh.Shared.Model
{
    public class PatientStatusExtract : Entity
    {
        public string ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }
        public Guid PatientId { get; set; }

        public PatientStatusExtract()
        {
           
        }

        public PatientStatusExtract(string exitDescription, DateTime? exitDate, string exitReason, string emr, string project, Guid patientId)
        {
            ExitDescription = exitDescription;
            ExitDate = exitDate;
            ExitReason = exitReason;
            Emr = emr;
            Project = project;
            PatientId = patientId;
        }
    }
}
