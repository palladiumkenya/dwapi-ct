using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.DWapi.Client.Model.DTO;

namespace PalladiumDwh.DWapi.Client.Model.Profiles
{
    public class PatientStatusProfile
    {
        private Facility _facilityInfo;
        private PatientExtract _patientInfo;
        private List<PatientStatusExtract> _patientStatusExtracts;

        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public List<PatientStatusExtractDTO> StatusExtracts { get; set; } = new List<PatientStatusExtractDTO>();

        public Facility FacilityInfo => _facilityInfo;
        public PatientExtract PatientInfo => _patientInfo;
        public List<PatientStatusExtract> PatientStatusExtracts => _patientStatusExtracts;

        public void GeneratePatientRecord()
        {
            _facilityInfo = Facility.GenerateFacility();
            _patientInfo = Demographic.GeneratePatient(_facilityInfo.Id);
        }

        public void GenerateRecords()
        {
            _patientStatusExtracts = new List<PatientStatusExtract>();
            foreach (var e in StatusExtracts)
            {
                _patientStatusExtracts.Add(e.GeneratePatientStatusExtract(_patientInfo.Id));
            }
        }

        public static PatientStatusProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientStatusProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                StatusExtracts =
                    new PatientStatusExtractDTO().GeneratePatientStatusExtractDtOs(patient.PatientStatusExtracts).ToList()
            };
            return patientProfile;
        }
    }
}
