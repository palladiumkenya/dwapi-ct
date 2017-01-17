using System.Collections.Generic;
using PalladiumDwh.Core.Model.DTO;

namespace PalladiumDwh.Core.Model.Profiles
{
    public class PatientLabProfile
    {
        private Facility _facilityInfo;
        private PatientExtract _patientInfo;

        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public List<PatientLaboratoryExtractDTO> LaboratoryExtracts { get; set; } = new List<PatientLaboratoryExtractDTO>();


        public Facility FacilityInfo => _facilityInfo;
        public PatientExtract PatientInfo => _patientInfo;

        public void GeneratePatientRecord()
        {
            _facilityInfo = Facility.GenerateFacility();
            _patientInfo = Demographic.GeneratePatient(_facilityInfo.Id);
            var extracts = new List<PatientLaboratoryExtract>();
            foreach (var e in LaboratoryExtracts)
            {
                extracts.Add(e.GeneratePatientLaboratoryExtract(_patientInfo.Id));
            }
            _patientInfo.AddPatientLaboratoryExtracts(extracts);
        }
    }
}
