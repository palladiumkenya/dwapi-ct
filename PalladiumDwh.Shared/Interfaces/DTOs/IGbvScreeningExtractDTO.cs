using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public   interface IGbvScreeningExtractDTO : IExtractDTO,IGbvScreening
    {
        Guid PatientId { get; set; }
    }
}