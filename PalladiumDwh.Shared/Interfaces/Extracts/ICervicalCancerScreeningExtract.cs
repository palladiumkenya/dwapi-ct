using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public interface ICervicalCancerScreeningExtract : IExtract,ICervicalCancerScreening
    {
        Guid PatientId { get; set; }
    }
}
