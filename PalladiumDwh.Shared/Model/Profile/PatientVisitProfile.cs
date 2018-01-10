using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.Profiles;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.Profile
{
    public class PatientVisitProfile :ExtractProfile<PatientVisitExtract>, IPatientVisitProfile
    {
        public List<PatientVisitExtractDTO> VisitExtracts { get; set; } = new List<PatientVisitExtractDTO>();

        public static PatientVisitProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientVisitProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                VisitExtracts =
                    new PatientVisitExtractDTO().GeneratePatientVisitExtractDtOs(patient.PatientVisitExtracts).ToList()
            };
            return patientProfile;
        }
        public override bool IsValid()
        {
            return base.IsValid() && VisitExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != VisitExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);

            foreach (var e in VisitExtracts)
                Extracts.Add(e.GeneratePatientVisitExtract(PatientInfo.Id));
        }
    }
}