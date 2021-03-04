using System;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
{
    public class PatientStatusExtract : Entity, IPatientStatusExtract
    {
        public string ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? Created { get; set; }
        public string TOVerified { get; set; }
        public DateTime? TOVerifiedDate { get; set; }
        public PatientStatusExtract()
        {
            Created = DateTime.Now;
        }



        public PatientStatusExtract(string exitDescription, DateTime? exitDate, string exitReason, Guid patientId, string emr, string project,
            string toVerified, DateTime? toVerifiedDate)
        {
            ExitDescription = exitDescription;
            ExitDate = exitDate;
            ExitReason = exitReason;
            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;

            TOVerified = toVerified;
            TOVerifiedDate = toVerifiedDate;
        }


    }
}
