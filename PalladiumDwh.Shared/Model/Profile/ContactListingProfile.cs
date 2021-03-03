using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Interfaces.Profiles;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.Profile
{
    public class ContactListingProfile :ExtractProfile<ContactListingExtract>, IContactListingProfile
    {
        public List<ContactListingExtractDTO> ContactListingExtracts { get; set; } = new List<ContactListingExtractDTO>();

        public static ContactListingProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new ContactListingProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                ContactListingExtracts =
                    new ContactListingExtractDTO().GenerateContactListingExtractDtOs(patient.ContactListingExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<ContactListingProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles=new List<ContactListingProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && ContactListingExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != ContactListingExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in ContactListingExtracts)
                Extracts.Add(e.GenerateContactListingExtract(PatientInfo.Id));
        }
    }
}
