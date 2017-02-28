using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public interface IPatientArtExtractDTO:IExtractDTO,IArt
    {
        Guid PatientId { get; set; }
    }
}