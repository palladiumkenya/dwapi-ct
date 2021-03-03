using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Interfaces.Profiles;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.Profile
{
    public class EnhancedAdherenceCounsellingProfile :ExtractProfile<EnhancedAdherenceCounsellingExtract>, IEnhancedAdherenceCounsellingProfile
    {
        public List<EnhancedAdherenceCounsellingExtractDTO> EnhancedAdherenceCounsellingExtracts { get; set; } = new List<EnhancedAdherenceCounsellingExtractDTO>();

        public static EnhancedAdherenceCounsellingProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new EnhancedAdherenceCounsellingProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                EnhancedAdherenceCounsellingExtracts =
                    new EnhancedAdherenceCounsellingExtractDTO().GenerateEnhancedAdherenceCounsellingExtractDtOs(patient.EnhancedAdherenceCounsellingExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<EnhancedAdherenceCounsellingProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles=new List<EnhancedAdherenceCounsellingProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && EnhancedAdherenceCounsellingExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != EnhancedAdherenceCounsellingExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in EnhancedAdherenceCounsellingExtracts)
                Extracts.Add(e.GenerateEnhancedAdherenceCounsellingExtract(PatientInfo.Id));
        }
    }
}
