using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Interfaces.Profiles;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.Profile
{
    public class IITRiskScoresProfile :ExtractProfile<IITRiskScoresExtract>, IIITRiskScoresProfile
    {
        public List<IITRiskScoresExtractDTO> IITRiskScoresExtracts { get; set; } = new List<IITRiskScoresExtractDTO>();

        public static IITRiskScoresProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new IITRiskScoresProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                IITRiskScoresExtracts =
                    new IITRiskScoresExtractDTO().GenerateIITRiskScoresExtractDtOs(patient.IITRiskScoresExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<IITRiskScoresProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles=new List<IITRiskScoresProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && IITRiskScoresExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != IITRiskScoresExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in IITRiskScoresExtracts)
                Extracts.Add(e.GenerateIITRiskScoresExtract(PatientInfo.Id));
        }
    }
}
