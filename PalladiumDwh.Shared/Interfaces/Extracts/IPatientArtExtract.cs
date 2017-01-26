using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public interface IPatientArtExtract: IExtract,IArt
    {
        Guid PatientId { get; set; }
    }
}