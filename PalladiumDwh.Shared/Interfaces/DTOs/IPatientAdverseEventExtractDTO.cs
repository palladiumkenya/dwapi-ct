using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public interface IPatientAdverseEventExtractDTO : IExtractDTO, IAdverseEvent
    {
        Guid PatientId { get; set; }
    }
}