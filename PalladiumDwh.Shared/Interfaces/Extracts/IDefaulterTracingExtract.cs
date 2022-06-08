using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public  interface IDefaulterTracingExtract : IExtract,IDefaulterTracing
    {
        Guid PatientId { get; set; }
    }
}