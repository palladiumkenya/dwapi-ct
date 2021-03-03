using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{

    public interface IOvcExtractDTO : IExtractDTO,IOvc
    {
        Guid PatientId { get; set; }
    }
}
