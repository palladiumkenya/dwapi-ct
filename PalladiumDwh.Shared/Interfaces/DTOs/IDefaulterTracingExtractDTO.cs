using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public  interface IDefaulterTracingExtractDTO : IExtractDTO,IDefaulterTracing
    {
        Guid PatientId { get; set; }
    }
}