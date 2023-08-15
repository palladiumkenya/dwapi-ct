using System.Collections.Generic;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Interfaces.Profiles
{
    public interface IIITRiskScoresProfile:IExtractProfile<IITRiskScoresExtract>{List<IITRiskScoresExtractDTO> IITRiskScoresExtracts { get; set; }}
}
