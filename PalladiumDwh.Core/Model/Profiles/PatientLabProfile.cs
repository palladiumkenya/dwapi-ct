using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Model.DTO;

namespace PalladiumDwh.Core.Model.Profiles
{
    public class PatientLabProfile
    {
        private Facility _facilityInfo;
        private PatientExtract _patientInfo;
        private List<PatientLaboratoryExtract> _patientLaboratoryExtracts;

        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public List<PatientLaboratoryExtractDTO> LaboratoryExtracts { get; set; } = new List<PatientLaboratoryExtractDTO>();


        public Facility FacilityInfo => _facilityInfo;
        public PatientExtract PatientInfo => _patientInfo;
        public List<PatientLaboratoryExtract> PatientLaboratoryExtracts => _patientLaboratoryExtracts;

        public void GeneratePatientRecord()
        {
            _facilityInfo = Facility.GenerateFacility();
            _patientInfo = Demographic.GeneratePatient(_facilityInfo.Id);
        }

        public void GenerateRecords()
        {
            _patientLaboratoryExtracts = new List<PatientLaboratoryExtract>();
            foreach (var e in LaboratoryExtracts)
            {
                _patientLaboratoryExtracts.Add(e.GeneratePatientLaboratoryExtract(_patientInfo.Id));
            }
        }

        public static PatientLabProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientLabProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                LaboratoryExtracts =
                    new PatientLaboratoryExtractDTO().GenerateLaboratoryExtractDtOs(patient.PatientLaboratoryExtracts).ToList()
            };
            return patientProfile;
        }
    }
}
