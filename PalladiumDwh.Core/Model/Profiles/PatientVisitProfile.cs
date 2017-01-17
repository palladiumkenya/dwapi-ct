using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Model.DTO;

namespace PalladiumDwh.Core.Model.Profiles
{
    public class PatientVisitProfile
    {
        private Facility _facilityInfo;
        private PatientExtract _patientInfo;
        private List<PatientVisitExtract> _patientVisitExtracts;

        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public List<PatientVisitExtractDTO> VisitExtracts { get; set; }=new List<PatientVisitExtractDTO>();

        public Facility FacilityInfo => _facilityInfo;
        public PatientExtract PatientInfo => _patientInfo;
        public List<PatientVisitExtract> PatientVisitExtracts => _patientVisitExtracts;

        public void GeneratePatientRecord()
        {
            _facilityInfo = Facility.GenerateFacility();
            _patientInfo = Demographic.GeneratePatient(_facilityInfo.Id);
        }

        public void GenerateRecords()
        {
            _patientVisitExtracts = new List<PatientVisitExtract>();
            foreach (var e in VisitExtracts)
            {
                _patientVisitExtracts.Add(e.GeneratePatientVisitExtract(_patientInfo.Id));
            }
        }

        public static PatientVisitProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientVisitProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                VisitExtracts =
                    new PatientVisitExtractDTO().GeneratePatientVisitExtractDtOs(patient.PatientVisitExtracts).ToList()
            };
            return patientProfile;
        }
    }
}
