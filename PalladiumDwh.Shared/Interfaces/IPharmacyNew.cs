using System;

namespace PalladiumDwh.Shared.Interfaces
{
    public interface IPharmacyNew
    {
        string RegimenChangedSwitched { get; set; }
        string RegimenChangeSwitchReason { get; set; }
        string StopRegimenReason { get; set; }
        DateTime? StopRegimenDate { get; set; }
    }
}
