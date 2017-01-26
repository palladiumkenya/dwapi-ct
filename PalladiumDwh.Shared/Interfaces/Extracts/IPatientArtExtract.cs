using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public interface IPatientArtExtract: IExtract,IArt
    {
        string PatientSource { get; set; }
        Guid PatientId { get; set; }
    }
}