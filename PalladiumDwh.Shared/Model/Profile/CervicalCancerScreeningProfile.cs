using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Interfaces.Profiles;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.Profile
{
    public class CervicalCancerScreeningProfile :ExtractProfile<CervicalCancerScreeningExtract>, ICervicalCancerScreeningProfile
    {
        public List<CervicalCancerScreeningExtractDTO> CervicalCancerScreeningExtracts { get; set; } = new List<CervicalCancerScreeningExtractDTO>();

        public static CervicalCancerScreeningProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new CervicalCancerScreeningProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                CervicalCancerScreeningExtracts =
                    new CervicalCancerScreeningExtractDTO().GenerateCervicalCancerScreeningExtractDtOs(patient.CervicalCancerScreeningExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<CervicalCancerScreeningProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles=new List<CervicalCancerScreeningProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && CervicalCancerScreeningExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != CervicalCancerScreeningExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in CervicalCancerScreeningExtracts)
                Extracts.Add(e.GenerateCervicalCancerScreeningExtract(PatientInfo.Id));
        }
    }
}