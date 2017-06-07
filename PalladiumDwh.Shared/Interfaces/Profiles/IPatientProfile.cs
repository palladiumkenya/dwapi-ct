using System.Collections.Generic;
using PalladiumDwh.Shared.Model.DTO;

namespace PalladiumDwh.Shared.Interfaces.Profiles
{
    public interface IPatientProfile:IProfile
    {
        List<PatientArtExtractDTO> ArtExtracts { get; set; }
        List<PatientBaselinesExtractDTO> BaselinesExtracts { get; set; }
        List<PatientLaboratoryExtractDTO> LaboratoryExtracts { get; set; }
        List<PatientPharmacyExtractDTO> PharmacyExtracts { get; set; }
        List<PatientStatusExtractDTO> StatusExtracts { get; set; }
        List<PatientVisitExtractDTO> VisitExtracts { get; set; }
    }
}