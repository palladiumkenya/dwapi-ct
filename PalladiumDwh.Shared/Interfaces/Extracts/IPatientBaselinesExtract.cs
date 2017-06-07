using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public interface IPatientBaselinesExtract: IExtract,IBaseline
    {
        Guid PatientId { get; set; }
    }
}