using System;

namespace PalladiumDwh.Shared.Interfaces
{
    public interface IPharmacyNew
    {
        string RegimenChangedSwitched { get; set; }
        string RegimenChangeSwitchReason { get; set; }
        string StopRegimenReason { get; set; }
        DateTime? StopRegimenDate { get; set; }
        DateTime? Date_Created { get; set; } 
        DateTime? Date_Last_Modified { get; set; } 
        string RecordUUID { get; set; }

    }
}
