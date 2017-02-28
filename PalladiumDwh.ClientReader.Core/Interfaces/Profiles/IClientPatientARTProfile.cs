using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.DTO;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Profiles
{
    public interface IClientPatientARTProfile:IClientExtractProfile
    {
        List<ClientPatientArtExtractDTO> ArtExtracts { get; set; }
    }
}