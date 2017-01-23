using System.Collections.Generic;
using PalladiumDwh.Shared.Model.DTO;

namespace PalladiumDwh.Shared.Model.Profiles
{
    public class PatientProfile
    {
        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public List<PatientArtExtractDTO> ArtExtracts { get; set; } = new List<PatientArtExtractDTO>();
        public List<PatientBaselinesExtractDTO> BaselinesExtracts { get; set; } = new List<PatientBaselinesExtractDTO>();
        public List<PatientLaboratoryExtractDTO> LaboratoryExtracts { get; set; } =new List<PatientLaboratoryExtractDTO>();
        public List<PatientPharmacyExtractDTO> PharmacyExtracts { get; set; } = new List<PatientPharmacyExtractDTO>();
        public List<PatientStatusExtractDTO> StatusExtracts { get; set; } = new List<PatientStatusExtractDTO>();
        public List<PatientVisitExtractDTO> VisitExtracts { get; set; } = new List<PatientVisitExtractDTO>();


        public Facility FacilityInfo { get;  set; }
        public PatientExtract PatientInfo { get;  set; }

        public void GeneratePatientRecord()
        {
            FacilityInfo = Facility.GenerateFacility();
            PatientInfo = Demographic.GeneratePatient(FacilityInfo.Id);
        }

        public static PatientProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient)
            };
            return patientProfile;
        }
        public override string ToString()
        {
            return $"{PatientInfo.Id}";
        }
    }
}