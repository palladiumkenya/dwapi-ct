using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PalladiumDwh.ClientReader.Core.Interfaces.Source;

namespace PalladiumDwh.ClientReader.Core.Model.Source
{
    [Table("vTempPatientArtExtractError")]
    public class TempPatientArtExtractError : TempExtract, ITempPatientArtExtract
    {

        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }

        public string FacilityName { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public DateTime? DOB { get; set; }
        public decimal? AgeEnrollment { get; set; }
        public decimal? AgeARTStart { get; set; }
        public decimal? AgeLastVisit { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string PatientSource { get; set; }
        public string Gender { get; set; }
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
        public string Provider { get; set; }
        public DateTime? LastVisit { get; set; }
        public string ExitReason { get; set; }
        public DateTime? ExitDate { get; set; }

        public virtual ICollection<TempPatientArtExtractErrorSummary> TempPatientArtExtractErrorSummaries { get; set; } = new List<TempPatientArtExtractErrorSummary>();
        public string PreviousARTUse { get; set; }
        public string PreviousARTPurpose { get; set; }
        public DateTime? DateLastUsed { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }
        public bool Voided { get; set; }

    }
}
