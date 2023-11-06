using System.Collections.Generic;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Interfaces.Profiles
{
    public interface ICervicalCancerScreeningProfile:IExtractProfile<CervicalCancerScreeningExtract>{List<CervicalCancerScreeningExtractDTO> CervicalCancerScreeningExtracts { get; set; }}
}