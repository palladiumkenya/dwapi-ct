using System.Collections.Generic;
using PalladiumDwh.Core.Model.DTO;

namespace PalladiumDwh.Core.Model.Profiles
{
    public class PatientPharmacyProfile
    {
        private Facility _facilityInfo;
        private PatientExtract _patientInfo;

        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public List<PatientPharmacyExtractDTO> PharmacyExtracts { get; set; } = new List<PatientPharmacyExtractDTO>();

        public Facility FacilityInfo => _facilityInfo;
        public PatientExtract PatientInfo => _patientInfo;

        public void GeneratePatientRecord()
        {
            _facilityInfo = Facility.GenerateFacility();
            _patientInfo = Demographic.GeneratePatient(_facilityInfo.Id);
            var extracts = new List<PatientPharmacyExtract>();
            foreach (var e in PharmacyExtracts)
            {
                extracts.Add(e.GeneratePatientPharmacyExtract(_patientInfo.Id));
            }
            _patientInfo.AddPatientPharmacyExtracts(extracts);
        }
    }
}
