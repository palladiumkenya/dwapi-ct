using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public    interface IOtzExtractDTO : IExtractDTO,IOtz
    {
        Guid PatientId { get; set; }
    }
}