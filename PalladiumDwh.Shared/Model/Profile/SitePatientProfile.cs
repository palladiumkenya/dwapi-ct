using System.Collections.Generic;
using Newtonsoft.Json;
using PalladiumDwh.Shared.Interfaces.Profiles;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.Profile
{
    public class SitePatientProfile 
    {
        [JsonIgnore]
        public Manifest Manifest { get; set; }

        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }

        public List<PatientArtExtractDTO> ArtExtracts { get; set; } = new List<PatientArtExtractDTO>();
        public List<PatientBaselinesExtractDTO> BaselinesExtracts { get; set; } = new List<PatientBaselinesExtractDTO>();
        public List<PatientLaboratoryExtractDTO> LaboratoryExtracts { get; set; } =new List<PatientLaboratoryExtractDTO>();
        public List<PatientPharmacyExtractDTO> PharmacyExtracts { get; set; } = new List<PatientPharmacyExtractDTO>();
        public List<PatientStatusExtractDTO> StatusExtracts { get; set; } = new List<PatientStatusExtractDTO>();
        public List<PatientVisitExtractDTO> VisitExtracts { get; set; } = new List<PatientVisitExtractDTO>();

        public bool IsValid()
        {
            return true;
        }
    }
}