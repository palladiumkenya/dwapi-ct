using System;

namespace PalladiumDwh.Shared.Interfaces
{
    public interface IStatus
    {
        string ExitDescription { get; set; }
        DateTime? ExitDate { get; set; }
        string ExitReason { get; set; }
    }
}