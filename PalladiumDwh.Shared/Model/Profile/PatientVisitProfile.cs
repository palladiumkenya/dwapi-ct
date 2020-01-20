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

        public static List<PatientVisitProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles=new List<PatientVisitProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
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
