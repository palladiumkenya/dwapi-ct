using System.Collections.Generic;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Interfaces.Profiles
{
    public interface IPatientPharmacyProfile:IExtractProfile<PatientPharmacyExtract>
    {
        List<PatientPharmacyExtractDTO> PharmacyExtracts { get; set; }
      
    }
}