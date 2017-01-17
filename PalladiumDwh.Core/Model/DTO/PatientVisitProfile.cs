using System.Collections.Generic;

namespace PalladiumDwh.Core.Model.DTO
{
    public class PatientVisitProfile
    {
        private Facility _facilityInfo;
        private PatientExtract _patientInfo;

        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
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
