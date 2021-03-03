using System;
using Dwapi.Contracts.Ct;

namespace PalladiumDwh.Shared.Interfaces
{
    public interface IStatus:IStatusNew
    {
        string ExitDescription { get; set; }
        DateTime? ExitDate { get; set; }
        string ExitReason { get; set; }
    }
}
