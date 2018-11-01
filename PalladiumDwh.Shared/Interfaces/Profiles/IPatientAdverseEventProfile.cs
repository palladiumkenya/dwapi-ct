using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Interfaces.Profiles
{
    public interface IPatientAdverseEventProfile : IExtractProfile<PatientAdverseEventExtract>
    {
        List<PatientAdverseEventExtractDTO> AdverseEventExtracts { get; set; }
    }
}