using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public interface ICervicalCancerScreeningExtractDTO : IExtractDTO,ICervicalCancerScreening
    {
        Guid PatientId { get; set; }
    }
}