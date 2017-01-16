using System;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Model.DTO
{
    public class PatientStatusExtractDTO
    {
        public string ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }

        public PatientStatusExtract GeneratePatientStatusExtract()
        {
            return new PatientStatusExtract(ExitDescription, ExitDate, ExitReason, Emr, Project, PatientId);
        }
    }
}
