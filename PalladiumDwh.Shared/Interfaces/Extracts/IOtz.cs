using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public    interface IOtzExtract : IExtract,IOtz
    {
          Guid PatientId { get; set; }
    }
}
