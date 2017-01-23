using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Model.DTO;

namespace PalladiumDwh.Shared.Model.Profiles
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
        public bool IsValid()
        {
            if (HasData())
                return
                    Facility.IsValid() &&
                    Demographic.IsValid() &&
                    VisitExtracts.Count > 0;
            return false;
        }

        public bool HasData()
        {
            return null != Facility && null != Demographic && null != VisitExtracts;
        }
        public override string ToString()
        {
            return $"{PatientInfo.Id}";
        }
    }
}