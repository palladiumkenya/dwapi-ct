using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public  interface IEnhancedAdherenceCounsellingExtractDTO : IExtractDTO,IEnhancedAdherenceCounselling
    {
        Guid PatientId { get; set; }
    }
}