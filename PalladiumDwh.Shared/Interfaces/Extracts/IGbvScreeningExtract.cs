using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public   interface IGbvScreeningExtract : IExtract,IGbvScreening
    {
         Guid PatientId { get; set; }
    }
}
