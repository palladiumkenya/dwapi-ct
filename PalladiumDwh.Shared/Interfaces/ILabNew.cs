using System;

namespace PalladiumDwh.Shared.Interfaces
{
    public interface ILabNew
    {
        DateTime? DateSampleTaken { get; set; }
        string SampleType { get; set; }
        DateTime? Date_Created { get; set; } 
        DateTime? Date_Last_Modified { get; set; } 
        string PatientUUID { get; set; }

    }
}
