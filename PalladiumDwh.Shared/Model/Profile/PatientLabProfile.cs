using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.Profiles;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.Profile
{
    public class PatientLabProfile : ExtractProfile<PatientLaboratoryExtract>, IPatientLabProfile
    {
        public List<PatientLaboratoryExtractDTO> LaboratoryExtracts { get; set; } =new List<PatientLaboratoryExtractDTO>();

        public static PatientLabProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientLabProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                LaboratoryExtracts =
                    new PatientLaboratoryExtractDTO().GenerateLaboratoryExtractDtOs(patient.PatientLaboratoryExtracts)
                        .ToList()
            };
            return patientProfile;
        }
        public override bool IsValid()
        {
            return base.IsValid() && LaboratoryExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != LaboratoryExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in LaboratoryExtracts)
                Extracts.Add(e.GeneratePatientLaboratoryExtract(PatientInfo.Id));
        }
    }
}