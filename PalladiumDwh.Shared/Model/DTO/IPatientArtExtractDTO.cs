using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Shared.Model.DTO
{
    public interface IPatientArtExtractDTO:IExtractDTO,IArt
    {
        string PatientSource { get; set; }
        Guid PatientId { get; set; }
    }
}