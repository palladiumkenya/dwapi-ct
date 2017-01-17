using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Model.DTO;

namespace PalladiumDwh.Core.Model.Profiles
{
    public class PatientBaselineProfile
    {
        private Facility _facilityInfo;
        private PatientExtract _patientInfo;
        private List<PatientBaselinesExtract> _patientBaselinesExtracts;

        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public List<PatientBaselinesExtractDTO> BaselinesExtracts { get; set; } = new List<PatientBaselinesExtractDTO>();
        
        public Facility FacilityInfo => _facilityInfo;
        public PatientExtract PatientInfo => _patientInfo;
        public List<PatientBaselinesExtract> PatientBaselinesExtracts => _patientBaselinesExtracts;

        public void GeneratePatientRecord()
        {
            _facilityInfo = Facility.GenerateFacility();
            _patientInfo = Demographic.GeneratePatient(_facilityInfo.Id);
        }

        public void GenerateRecords()
        {
            _patientBaselinesExtracts = new List<PatientBaselinesExtract>();
            foreach (var e in BaselinesExtracts)
            {
                _patientBaselinesExtracts.Add(e.GeneratePatientBaselinesExtract(_patientInfo.Id));
            }
        }

        public static PatientBaselineProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientBaselineProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                BaselinesExtracts =
                    new PatientBaselinesExtractDTO().GeneratePatientBaselinesExtractDtOs(patient.PatientBaselinesExtracts).ToList()
            };
            return patientProfile;
        }
    }
}

