using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.DWapi.Client.Model.DTO;

namespace PalladiumDwh.DWapi.Client.Model.Profiles
{
    public class PatientARTProfile
    {
        private Facility _facilityInfo;
        private PatientExtract _patientInfo;
        private List<PatientArtExtract> _patientArtExtracts=new List<PatientArtExtract>();

        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public List<PatientArtExtractDTO> ArtExtracts { get; set; } = new List<PatientArtExtractDTO>();

        public Facility FacilityInfo => _facilityInfo;
        public PatientExtract PatientInfo => _patientInfo;
        public List<PatientArtExtract> PatientArtExtracts => _patientArtExtracts;

        public void GeneratePatientRecord()
        {
            _facilityInfo = Facility.GenerateFacility();
            _patientInfo = Demographic.GeneratePatient(_facilityInfo.Id);
        }

        public void GenerateRecords()
        {
            _patientArtExtracts = new List<PatientArtExtract>();
            foreach (var e in ArtExtracts)
            {
                _patientArtExtracts.Add(e.GeneratePatientArtExtract(_patientInfo.Id));
            }
        }

        public static PatientARTProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientARTProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                ArtExtracts =
                    new PatientArtExtractDTO().GeneratePatientArtExtractDtOs(patient.PatientArtExtracts).ToList()
            };
            return patientProfile;
        }

        public override string ToString()
        {
            return $"{_patientInfo.Id}";
        }
    }
}
