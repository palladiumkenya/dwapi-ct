using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.Profiles;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.Profile
{
    public class PatientBaselineProfile :ExtractProfile<PatientBaselinesExtract> ,IPatientBaselineProfile
    {
        public List<PatientBaselinesExtractDTO> BaselinesExtracts { get; set; } = new List<PatientBaselinesExtractDTO>();



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

        public override bool IsValid()
        {
            return base.IsValid() && BaselinesExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != BaselinesExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in BaselinesExtracts)
                Extracts.Add(e.GeneratePatientBaselinesExtract(PatientInfo.Id));
        }
    }
}