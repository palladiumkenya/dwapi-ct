using System;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Model
{
    public class PatientArtExtract:Entity
    {
        
        public decimal? AgeEnrollment { get; set; }
        public decimal? AgeARTStart { get; set; }
        public decimal? AgeLastVisit { get; set; }       
        public DateTime? RegistrationDate { get; set; }
        public string PatientSource { get; set; }
        public DateTime? StartARTDate { get; set; }
        public DateTime? PreviousARTStartDate { get; set; }
        public string PreviousARTRegimen { get; set; }
        public DateTime? StartARTAtThisFacility { get; set; }
        public string StartRegimen { get; set; }
        public string StartRegimenLine { get; set; }
        public DateTime? LastARTDate { get; set; }
        public string LastRegimen { get; set; }
        public string LastRegimenLine { get; set; }
        public decimal? Duration { get; set; }
        public DateTime? ExpectedReturn { get; set; }
        public DateTime? LastVisit { get; set; }
        public string ExitReason { get; set; }
        public DateTime? ExitDate { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
    }
}
