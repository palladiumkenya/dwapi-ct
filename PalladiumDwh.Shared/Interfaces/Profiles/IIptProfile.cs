using System.Collections.Generic;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Interfaces.Profiles
{
    public interface IIptProfile:IExtractProfile<IptExtract>{List<IptExtractDTO> IptExtracts { get; set; }}
}