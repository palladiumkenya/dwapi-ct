using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public  interface IIPtExtract : IExtract,IIPt
    {
         Guid PatientId { get; set; }
    }
}
