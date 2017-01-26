using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Shared.Model
{
    public interface IPatientArtExtract: IExtract,IArt
    {
        string PatientSource { get; set; }
        Guid PatientId { get; set; }
    }
}