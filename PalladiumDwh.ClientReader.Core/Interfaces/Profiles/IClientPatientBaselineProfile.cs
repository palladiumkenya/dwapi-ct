using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model.DTO;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Profiles
{
    public interface IClientPatientBaselineProfile : IClientExtractProfile
    {
        List<ClientPatientBaselinesExtractDTO> BaselinesExtracts { get; set; }
    }
}