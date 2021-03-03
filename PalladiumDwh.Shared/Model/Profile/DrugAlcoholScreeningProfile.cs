using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Interfaces.Profiles;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.Profile
{
    public class DrugAlcoholScreeningProfile :ExtractProfile<DrugAlcoholScreeningExtract>, IDrugAlcoholScreeningProfile
    {
        public List<DrugAlcoholScreeningExtractDTO> DrugAlcoholScreeningExtracts { get; set; } = new List<DrugAlcoholScreeningExtractDTO>();

        public static DrugAlcoholScreeningProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new DrugAlcoholScreeningProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                DrugAlcoholScreeningExtracts =
                    new DrugAlcoholScreeningExtractDTO().GenerateDrugAlcoholScreeningExtractDtOs(patient.DrugAlcoholScreeningExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<DrugAlcoholScreeningProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles=new List<DrugAlcoholScreeningProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && DrugAlcoholScreeningExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != DrugAlcoholScreeningExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in DrugAlcoholScreeningExtracts)
                Extracts.Add(e.GenerateDrugAlcoholScreeningExtract(PatientInfo.Id));
        }
    }
}
