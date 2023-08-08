using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using PalladiumDwh.ClientReader.Core.Interfaces.Source;

namespace PalladiumDwh.ClientReader.Core.Model.Source
{
    [Table("vTempPatientStatusExtractError")]
    public class TempPatientStatusExtractError : TempExtract, ITempPatientStatusExtract
    {


        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }

        public string FacilityName { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public string ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }

        public virtual ICollection<TempPatientStatusExtractErrorSummary> TempPatientStatusExtractErrorSummaries { get; set; } = new List<TempPatientStatusExtractErrorSummary>();
        public string TOVerified { get; set; }
        public DateTime? TOVerifiedDate { get; set; }
        public DateTime? ReEnrollmentDate { get; set; }
        public string ReasonForDeath { get; set; }
        public string SpecificDeathReason { get; set; }
        public DateTime? DeathDate { get; set; }
        public DateTime? EffectiveDiscontinuationDate { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string PatientUUID { get; set; }

    }
}
