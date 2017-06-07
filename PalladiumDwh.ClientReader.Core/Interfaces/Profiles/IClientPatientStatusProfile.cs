using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model.DTO;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Profiles
{
    public interface IClientPatientStatusProfile:IClientExtractProfile
    {
        List<ClientPatientStatusExtractDTO> StatusExtracts { get; set; }
    }
}