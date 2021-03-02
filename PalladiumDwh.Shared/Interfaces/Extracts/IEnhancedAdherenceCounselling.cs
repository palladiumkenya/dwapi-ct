using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public  interface IEnhancedAdherenceCounsellingExtract : IExtract,IEnhancedAdherenceCounselling
    {
        Guid PatientId { get; set; }
    }
}
