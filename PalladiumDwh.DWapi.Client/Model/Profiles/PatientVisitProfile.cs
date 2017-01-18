using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.DWapi.Client.Model.DTO;

namespace PalladiumDwh.DWapi.Client.Model.Profiles
{
    public class PatientVisitProfile
    {
        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public List<PatientVisitExtractDTO> VisitExtracts { get; set; } = new List<PatientVisitExtractDTO>();

        public Facility FacilityInfo { get;  set; }

        public PatientExtract PatientInfo { get;  set; }

        public List<PatientVisitExtract> PatientVisitExtracts { get;  set; }

        public void GeneratePatientRecord()
        {
            FacilityInfo = Facility.GenerateFacility();
            PatientInfo = Demographic.GeneratePatient(FacilityInfo.Id);
        }

        public void GenerateRecords(Guid patientId)
        {
            PatientInfo.Id = patientId;
            PatientVisitExtracts = new List<PatientVisitExtract>();
            foreach (var e in VisitExtracts)
                PatientVisitExtracts.Add(e.GeneratePatientVisitExtract(PatientInfo.Id));
        }

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
    }
}