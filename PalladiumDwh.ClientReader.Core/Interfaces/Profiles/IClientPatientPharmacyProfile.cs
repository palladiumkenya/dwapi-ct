using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model.DTO;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Profiles
{
    public interface IClientPatientPharmacyProfile:IClientExtractProfile
    {
        List<ClientPatientPharmacyExtractDTO> PharmacyExtracts { get; set; }
      
    }
}