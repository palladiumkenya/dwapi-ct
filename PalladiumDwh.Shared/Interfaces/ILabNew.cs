using System;

namespace Dwapi.Contracts.Ct
{
    public interface ILabNew
    {
        DateTime? DateSampleTaken { get; set; }
        string SampleType { get; set; }
    }
}
