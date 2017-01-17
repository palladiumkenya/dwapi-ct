using System.Collections.Generic;
using PalladiumDwh.DWapi.Client.Model.DTO;

namespace PalladiumDwh.DWapi.Client.Model.Profiles
{
    public class PatientProfile
    {
        private Facility _facilityInfo;
        private PatientExtract _patientInfo;

        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public List<PatientArtExtractDTO> ArtExtracts { get; set; }=new List<PatientArtExtractDTO>();
        public List<PatientBaselinesExtractDTO> BaselinesExtracts { get; set; }=new List<PatientBaselinesExtractDTO>();
        public List<PatientLaboratoryExtractDTO> LaboratoryExtracts { get; set; }=new List<PatientLaboratoryExtractDTO>();
        public List<PatientPharmacyExtractDTO> PharmacyExtracts { get; set; }=new List<PatientPharmacyExtractDTO>();
        public List<PatientStatusExtractDTO> StatusExtracts { get; set; }=new List<PatientStatusExtractDTO>();
        public List<PatientVisitExtractDTO> VisitExtracts { get; set; }=new List<PatientVisitExtractDTO>();


        public Facility FacilityInfo => _facilityInfo;
        public PatientExtract PatientInfo => _patientInfo;

        public void GeneratePatientRecord()
        {
            _facilityInfo = Facility.GenerateFacility();
            _patientInfo = Demographic.GeneratePatient(_facilityInfo.Id);
        }
    }
}
