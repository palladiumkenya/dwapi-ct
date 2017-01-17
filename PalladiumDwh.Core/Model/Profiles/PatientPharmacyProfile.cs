using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Model.DTO;

namespace PalladiumDwh.Core.Model.Profiles
{
    public class PatientPharmacyProfile
    {
        private Facility _facilityInfo;
        private PatientExtract _patientInfo;
        private List<PatientPharmacyExtract> _patientPharmacyExtracts;

        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public List<PatientPharmacyExtractDTO> PharmacyExtracts { get; set; } = new List<PatientPharmacyExtractDTO>();

        public Facility FacilityInfo => _facilityInfo;
        public PatientExtract PatientInfo => _patientInfo;
        public List<PatientPharmacyExtract> PatientPharmacyExtracts => _patientPharmacyExtracts;

        public void GeneratePatientRecord()
        {
            _facilityInfo = Facility.GenerateFacility();
            _patientInfo = Demographic.GeneratePatient(_facilityInfo.Id);
        }

        public void GenerateRecords()
        {
            _patientPharmacyExtracts = new List<PatientPharmacyExtract>();
            foreach (var e in PharmacyExtracts)
            {
                _patientPharmacyExtracts.Add(e.GeneratePatientPharmacyExtract(_patientInfo.Id));
            }
        }


        public static PatientPharmacyProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientPharmacyProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                PharmacyExtracts =
                    new PatientPharmacyExtractDTO().GeneratePatientPharmacyExtractDtOs(patient.PatientPharmacyExtracts).ToList()
            };
            return patientProfile;
        }
    }
}
