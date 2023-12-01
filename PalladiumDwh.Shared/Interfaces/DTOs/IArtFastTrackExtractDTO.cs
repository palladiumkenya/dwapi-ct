using System;

namespace PalladiumDwh.Shared.Interfaces.DTOs
{
    public interface IArtFastTrackExtractDTO : IExtractDTO,IArtFastTrack
    {
        Guid PatientId { get; set; }
    }
}