using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Interfaces.Profiles;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.Profile
{
    public class CancerScreeningProfile :ExtractProfile<CancerScreeningExtract>, ICancerScreeningProfile
    {
        public List<CancerScreeningExtractDTO> CancerScreeningExtracts { get; set; } = new List<CancerScreeningExtractDTO>();

        public static CancerScreeningProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new CancerScreeningProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                CancerScreeningExtracts =
                    new CancerScreeningExtractDTO().GenerateCancerScreeningExtractDtOs(patient.CancerScreeningExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<CancerScreeningProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles=new List<CancerScreeningProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && CancerScreeningExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != CancerScreeningExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in CancerScreeningExtracts)
                Extracts.Add(e.GenerateCancerScreeningExtract(PatientInfo.Id));
        }
    }
}
