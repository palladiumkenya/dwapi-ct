using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Interfaces.Profiles;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.Profile
{
    public class RelationshipsProfile :ExtractProfile<RelationshipsExtract>, IRelationshipsProfile
    {
        public List<RelationshipsExtractDTO> RelationshipsExtracts { get; set; } = new List<RelationshipsExtractDTO>();

        public static RelationshipsProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new RelationshipsProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                RelationshipsExtracts =
                    new RelationshipsExtractDTO().GenerateRelationshipsExtractDtOs(patient.RelationshipsExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<RelationshipsProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles=new List<RelationshipsProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && RelationshipsExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != RelationshipsExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in RelationshipsExtracts)
                Extracts.Add(e.GenerateRelationshipsExtract(PatientInfo.Id));
        }
    }
}
