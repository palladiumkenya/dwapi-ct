using System;

namespace PalladiumDwh.Shared.Interfaces
{
    public interface ILabNew
    {
        DateTime? DateSampleTaken { get; set; }
        string SampleType { get; set; }
    }
}
