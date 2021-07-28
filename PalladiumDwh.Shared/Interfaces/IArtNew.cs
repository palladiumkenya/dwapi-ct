using System;

namespace PalladiumDwh.Shared.Interfaces
{
    public interface IArtNew
    {
        string PreviousARTUse { get; set; }
        string  PreviousARTPurpose { get; set; }
        DateTime?  DateLastUsed { get; set; }
    }
}
