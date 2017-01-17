using System.Collections.Generic;
using PalladiumDwh.Core.Model.DTO;

namespace PalladiumDwh.Core.Model.Profiles
{
    public class PatientBaselineProfile
    {
        private Facility _facilityInfo;
        private PatientExtract _patientInfo;

        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public List<PatientBaselinesExtractDTO> BaselinesExtracts { get; set; } = new List<PatientBaselinesExtractDTO>();
        
        public Facility FacilityInfo => _facilityInfo;
        public PatientExtract PatientInfo => _patientInfo;

        public void GeneratePatientRecord()
        {
            _facilityInfo = Facility.GenerateFacility();
            _patientInfo = Demographic.GeneratePatient(_facilityInfo.Id);

            var extracts = new List<PatientBaselinesExtract>();
            foreach (var e in BaselinesExtracts)
            {
                extracts.Add(e.GeneratePatientBaselinesExtract(_patientInfo.Id));
            }
            _patientInfo.AddPatientBaselinesExtracts(extracts);
        }
    }
}
