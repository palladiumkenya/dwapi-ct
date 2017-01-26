using System;

namespace PalladiumDwh.Shared.Model
{
    public class PatientStatusExtract : Entity, IPatientStatusExtract
    {
        public string ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }
        public Guid PatientId { get; set; }

        public PatientStatusExtract()
        {
           
        }

        public PatientStatusExtract(string exitDescription, DateTime? exitDate, string exitReason, Guid patientId, string emr, string project)
        {
            ExitDescription = exitDescription;
            ExitDate = exitDate;
            ExitReason = exitReason;
            PatientId = patientId;
            Emr = emr;
            Project = project;
        }
    }
}
