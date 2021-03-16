using System;

namespace PalladiumDwh.Shared.Interfaces
{
    public interface IStatusNew
    {
        string TOVerified { get; set; }
        DateTime? TOVerifiedDate { get; set; }
        DateTime? ReEnrollmentDate { get; set; }
    }
}
