using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public interface IOvcExtract : IExtract,IOvc
    {
         Guid PatientId { get; set; }
    }
}
