using System.Collections.Generic;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Interfaces.Profiles
{
    public interface IOvcProfile:IExtractProfile<OvcExtract>{List<OvcExtractDTO> OvcExtracts { get; set; }}
}