using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.Profiles;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.Profile
{
    public class PatientPharmacyProfile :ExtractProfile<PatientPharmacyExtract>, IPatientPharmacyProfile
    {
        public List<PatientPharmacyExtractDTO> PharmacyExtracts { get; set; } = new List<PatientPharmacyExtractDTO>();

        public static PatientPharmacyProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientPharmacyProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                PharmacyExtracts =
                    new PatientPharmacyExtractDTO().GeneratePatientPharmacyExtractDtOs(patient.PatientPharmacyExtracts)
                        .ToList()
            };
            return patientProfile;
        }
        public override bool IsValid()
        {
            return base.IsValid() && PharmacyExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != PharmacyExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in PharmacyExtracts)
                Extracts.Add(e.GeneratePatientPharmacyExtract(PatientInfo.Id));
        }
    }
}