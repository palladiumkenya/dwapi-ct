using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.DTOs
{
    public interface IClientPatientArtExtractDTO:IClientExtractDTO,IArt
    {
        Guid PatientId { get; set; }
    }
}