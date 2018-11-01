using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.Profiles;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.Profile
{
    public class PatientAdverseEventProfile : ExtractProfile<PatientAdverseEventExtract>, IPatientAdverseEventProfile
    {
        public List<PatientAdverseEventExtractDTO> AdverseEventExtracts { get; set; }=new List<PatientAdverseEventExtractDTO>();

        public static PatientAdverseEventProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientAdverseEventProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                AdverseEventExtracts =
                    new PatientAdverseEventExtractDTO().GeneratePatientAdverseEventExtractDtOs(patient.PatientAdverseEventExtracts)
                        .ToList()
            };
            return patientProfile;
        }
        public override bool IsValid()
        {
            return base.IsValid() && AdverseEventExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != AdverseEventExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in AdverseEventExtracts)
                Extracts.Add(e.GeneratePatientAdverseEventExtract(PatientInfo.Id));
        }
    }
}