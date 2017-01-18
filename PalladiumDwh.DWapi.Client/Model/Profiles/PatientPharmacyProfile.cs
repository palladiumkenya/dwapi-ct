using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.DWapi.Client.Model.DTO;

namespace PalladiumDwh.DWapi.Client.Model.Profiles
{
    public class PatientPharmacyProfile
    {
        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public List<PatientPharmacyExtractDTO> PharmacyExtracts { get; set; } = new List<PatientPharmacyExtractDTO>();

        public Facility FacilityInfo { get;  set; }
        public PatientExtract PatientInfo { get;  set; }
        public List<PatientPharmacyExtract> PatientPharmacyExtracts { get;  set; }

        public void GeneratePatientRecord()
        {
            FacilityInfo = Facility.GenerateFacility();
            PatientInfo = Demographic.GeneratePatient(FacilityInfo.Id);
        }

        public void GenerateRecords(Guid patientId)
        {
            PatientInfo.Id = patientId;
            PatientPharmacyExtracts = new List<PatientPharmacyExtract>();
            foreach (var e in PharmacyExtracts)
                PatientPharmacyExtracts.Add(e.GeneratePatientPharmacyExtract(PatientInfo.Id));
        }


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
    }
}