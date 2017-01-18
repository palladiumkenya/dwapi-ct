using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.DWapi.Client.Model.DTO;

namespace PalladiumDwh.DWapi.Client.Model.Profiles
{
    public class PatientLabProfile
    {
        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public List<PatientLaboratoryExtractDTO> LaboratoryExtracts { get; set; } =new List<PatientLaboratoryExtractDTO>();


        public Facility FacilityInfo { get; set; }
        public PatientExtract PatientInfo { get; set; }
        public List<PatientLaboratoryExtract> PatientLaboratoryExtracts { get;  set; }

        public void GeneratePatientRecord()
        {
            FacilityInfo = Facility.GenerateFacility();
            PatientInfo = Demographic.GeneratePatient(FacilityInfo.Id);
        }

        public void GenerateRecords(Guid patientId)
        {
            PatientInfo.Id = patientId;
            PatientLaboratoryExtracts = new List<PatientLaboratoryExtract>();
            foreach (var e in LaboratoryExtracts)
                PatientLaboratoryExtracts.Add(e.GeneratePatientLaboratoryExtract(PatientInfo.Id));
        }

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
    }
}