using System;
using PalladiumDwh.Shared.Model.DTO;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public interface IPatientArtExtractDTO:IExtractDTO,IArt
    {
        string PatientSource { get; set; }
        Guid PatientId { get; set; }
    }
}