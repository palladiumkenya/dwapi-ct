using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Core.Model.DTO;

namespace PalladiumDwh.Core.Model.Profiles
{
    public class PatientStatusProfile
    {
        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public List<PatientStatusExtractDTO> StatusExtracts { get; set; } = new List<PatientStatusExtractDTO>();

        public Facility FacilityInfo { get;  set; }
        public PatientExtract PatientInfo { get;  set; }
        public List<PatientStatusExtract> PatientStatusExtracts { get;  set; }

        public void GeneratePatientRecord()
        {
            FacilityInfo = Facility.GenerateFacility();
            PatientInfo = Demographic.GeneratePatient(FacilityInfo.Id);
        }

        public void GenerateRecords(Guid patientId)
        {
            PatientInfo.Id = patientId;
            PatientStatusExtracts = new List<PatientStatusExtract>();
            foreach (var e in StatusExtracts)
                PatientStatusExtracts.Add(e.GeneratePatientStatusExtract(PatientInfo.Id));
        }

        public static PatientStatusProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientStatusProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                StatusExtracts =
                    new PatientStatusExtractDTO().GeneratePatientStatusExtractDtOs(patient.PatientStatusExtracts)
                        .ToList()
            };
            return patientProfile;
        }
        public override string ToString()
        {
            return $"{PatientInfo.Id}";
        }
    }
}