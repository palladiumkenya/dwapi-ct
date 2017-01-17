using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Model.DTO;

namespace PalladiumDwh.Core.Model.Profiles
{
    public class PatientARTProfile
    {
        private Facility _facilityInfo;
        private PatientExtract _patientInfo;

        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public List<PatientArtExtractDTO> ArtExtracts { get; set; } = new List<PatientArtExtractDTO>();

        public Facility FacilityInfo => _facilityInfo;
        public PatientExtract PatientInfo => _patientInfo;

        public void GeneratePatientRecord()
        {
            _facilityInfo = Facility.GenerateFacility();
            _patientInfo = Demographic.GeneratePatient(_facilityInfo.Id);
        }

        public void GenerateArtRecords()
        {
            var extracts = new List<PatientArtExtract>();
            foreach (var e in ArtExtracts)
            {
                extracts.Add(e.GeneratePatientArtExtract(_patientInfo.Id));
            }
            _patientInfo.AddPatientArtExtracts(extracts);
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
    }
}
