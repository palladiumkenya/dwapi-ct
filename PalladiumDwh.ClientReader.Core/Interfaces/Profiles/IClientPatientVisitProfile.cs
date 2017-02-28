using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.DTO;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Profiles
{
    public interface IClientPatientVisitProfile:IClientExtractProfile
    {
        List<ClientPatientVisitExtractDTO> VisitExtracts { get; set; }
    }
}