using System.Collections.Generic;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Interfaces.Profiles
{
    public interface IPatientStatusProfile:IExtractProfile<PatientStatusExtract>
    {
        List<PatientStatusExtractDTO> StatusExtracts { get; set; }
    }
}