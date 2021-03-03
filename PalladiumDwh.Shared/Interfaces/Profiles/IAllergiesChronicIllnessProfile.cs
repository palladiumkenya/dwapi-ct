using System.Collections.Generic;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Interfaces.Profiles
{
    public interface IAllergiesChronicIllnessProfile:IExtractProfile<AllergiesChronicIllnessExtract>{List<AllergiesChronicIllnessExtractDTO> AllergiesChronicIllnessExtracts { get; set; }}
}
