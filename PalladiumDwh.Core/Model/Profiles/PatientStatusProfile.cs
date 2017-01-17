using System.Collections.Generic;
using PalladiumDwh.Core.Model.DTO;

namespace PalladiumDwh.Core.Model.Profiles
{
    public class PatientStatusProfile
    {
        private Facility _facilityInfo;
        private PatientExtract _patientInfo;

        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public List<PatientStatusExtractDTO> StatusExtracts { get; set; } = new List<PatientStatusExtractDTO>();

        public Facility FacilityInfo => _facilityInfo;
        public PatientExtract PatientInfo => _patientInfo;

        public void GeneratePatientRecord()
        {
            _facilityInfo = Facility.GenerateFacility();
            _patientInfo = Demographic.GeneratePatient(_facilityInfo.Id);


            var extracts = new List<PatientStatusExtract>();
            foreach (var e in StatusExtracts)
            {
                extracts.Add(e.GeneratePatientStatusExtract(_patientInfo.Id));
            }
            _patientInfo.AddPatientStatusExtracts(extracts);

        }
    }
}
