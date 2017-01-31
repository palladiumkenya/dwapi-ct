using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.DTO;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Profiles
{
    public interface IClientPatientLabProfile:IClientExtractProfile
    {
        List<ClientPatientLaboratoryExtractDTO> LaboratoryExtracts { get; set; }
      
    }
}