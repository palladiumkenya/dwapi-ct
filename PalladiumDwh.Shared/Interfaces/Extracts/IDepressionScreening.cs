using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public
     interface IDepressionScreeningExtract : IExtract,IDepressionScreening
    {
         Guid PatientId { get; set; }
    }
}
