using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Interfaces.Profiles;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.Profile
{
    public class ArtFastTrackProfile :ExtractProfile<ArtFastTrackExtract>, IArtFastTrackProfile
    {
        public List<ArtFastTrackExtractDTO> ArtFastTrackExtracts { get; set; } = new List<ArtFastTrackExtractDTO>();

        public static ArtFastTrackProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new ArtFastTrackProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                ArtFastTrackExtracts =
                    new ArtFastTrackExtractDTO().GenerateArtFastTrackExtractDtOs(patient.ArtFastTrackExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<ArtFastTrackProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles=new List<ArtFastTrackProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && ArtFastTrackExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != ArtFastTrackExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in ArtFastTrackExtracts)
                Extracts.Add(e.GenerateArtFastTrackExtract(PatientInfo.Id));
        }
    }
}
