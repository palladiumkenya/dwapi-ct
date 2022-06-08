using System.Collections.Generic;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Interfaces.Profiles
{
    public interface ICovidProfile : IExtractProfile<CovidExtract>
    {
        List<CovidExtractDTO> CovidExtracts { get; set; }
    }
}
