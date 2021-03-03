using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public  interface IIptExtract : IExtract,IIpt
    {
         Guid PatientId { get; set; }
    }
}
