using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public  interface IIptExtractDTO : IExtractDTO,IIpt
    {
        Guid PatientId { get; set; }
    }
}
