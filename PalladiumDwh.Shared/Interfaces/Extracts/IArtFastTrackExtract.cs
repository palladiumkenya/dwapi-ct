using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public interface IArtFastTrackExtract : IExtract,IArtFastTrack
    {
        Guid PatientId { get; set; }
    }
}
