using System;

namespace PalladiumDwh.Shared.Interfaces
{
    public interface IStatusNew
    {
        string TOVerified { get; set; }
        DateTime? TOVerifiedDate { get; set; }
        DateTime? ReEnrollmentDate { get; set; }

        string ReasonForDeath { get; set; }
        string SpecificDeathReason { get; set; }
        DateTime? DeathDate { get; set; }
        DateTime? EffectiveDiscontinuationDate { get; set; }
        DateTime? Date_Created { get; set; } 
        DateTime? Date_Last_Modified { get; set; } 
        string RecordUUID { get; set; }

    }
}
