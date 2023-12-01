using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public interface ICancerScreeningExtractDTO : IExtractDTO,ICancerScreening
    {
        Guid PatientId { get; set; }
    }
}