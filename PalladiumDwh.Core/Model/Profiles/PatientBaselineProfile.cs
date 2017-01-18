using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Model.DTO;

namespace PalladiumDwh.Core.Model.Profiles
{
    public class PatientBaselineProfile
    {
        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public List<PatientBaselinesExtractDTO> BaselinesExtracts { get; set; } = new List<PatientBaselinesExtractDTO>();

        public Facility FacilityInfo { get; set; }
        public PatientExtract PatientInfo { get; set; }
        public List<PatientBaselinesExtract> PatientBaselinesExtracts { get; set; }

        public void GeneratePatientRecord()
        {
            FacilityInfo = Facility.GenerateFacility();
            PatientInfo = Demographic.GeneratePatient(FacilityInfo.Id);
        }

        public void GenerateRecords(Guid patientId)
        {
            PatientInfo.Id = patientId;
            PatientBaselinesExtracts = new List<PatientBaselinesExtract>();
            foreach (var e in BaselinesExtracts)
                PatientBaselinesExtracts.Add(e.GeneratePatientBaselinesExtract(PatientInfo.Id));
        }

        public static PatientBaselineProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientBaselineProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                BaselinesExtracts =
                    new PatientBaselinesExtractDTO().GeneratePatientBaselinesExtractDtOs(
                        patient.PatientBaselinesExtracts).ToList()
            };
            return patientProfile;
        }
    }
}