using System;

namespace PalladiumDwh.Shared.Interfaces
{
    public interface IArtNew
    {
        string PreviousARTUse { get; set; }
        string  PreviousARTPurpose { get; set; }
        DateTime?  DateLastUsed { get; set; }
        DateTime? Date_Created { get; set; } 
        DateTime? Date_Last_Modified { get; set; } 
        string PatientUUID { get; set; }

    }
}
