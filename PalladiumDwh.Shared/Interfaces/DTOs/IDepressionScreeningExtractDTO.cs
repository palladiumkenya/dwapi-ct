using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public
        interface IDepressionScreeningExtractDTO : IExtractDTO,IDepressionScreening
    {
        Guid PatientId { get; set; }
    }
}