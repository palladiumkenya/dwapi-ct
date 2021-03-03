using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Interfaces.Profiles;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.Profile
{
    public class IptProfile :ExtractProfile<IptExtract>, IIptProfile
    {
        public List<IptExtractDTO> IptExtracts { get; set; } = new List<IptExtractDTO>();

        public static IptProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new IptProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                IptExtracts =
                    new IptExtractDTO().GenerateIptExtractDtOs(patient.IptExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<IptProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles=new List<IptProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && IptExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != IptExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in IptExtracts)
                Extracts.Add(e.GenerateIptExtract(PatientInfo.Id));
        }
    }
}
