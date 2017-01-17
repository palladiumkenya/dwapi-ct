using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PalladiumDwh.Core.Model.DTO
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
        }
    }
}
