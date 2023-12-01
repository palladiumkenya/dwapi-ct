using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public interface ICancerScreeningExtract : IExtract,ICancerScreening
    {
        Guid PatientId { get; set; }
    }
}
