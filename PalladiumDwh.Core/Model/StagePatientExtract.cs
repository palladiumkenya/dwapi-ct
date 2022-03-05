using System;
using PalladiumDwh.Core.Model.Bag;
using PalladiumDwh.Shared.Enum;
using PalladiumDwh.Shared.Interfaces.Extracts;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Model
{
    public class StagePatientExtract:IStagePatientExtract
    {
        public Guid Id { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public bool Voided { get; set; }
        public bool Processed { get; set; }
        public string Pkv { get; set; }
        public string Occupation { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? RegistrationAtCCC { get; set; }
        public DateTime? RegistrationATPMTCT { get; set; }
        public DateTime? RegistrationAtTBClinic { get; set; }
        public string Region { get; set; }
        public string PatientSource { get; set; }
        public string District { get; set; }
        public string Village { get; set; }
        public string ContactRelation { get; set; }
        public DateTime? LastVisit { get; set; }
        public string MaritalStatus { get; set; }
        public string EducationLevel { get; set; }
        public DateTime? DateConfirmedHIVPositive { get; set; }
        public string PreviousARTExposure { get; set; }
        public DateTime? PreviousARTStartDate { get; set; }
        public string StatusAtCCC { get; set; }
        public string StatusAtPMTCT { get; set; }
        public string StatusAtTBClinic { get; set; }
        public string Orphan { get; set; }
        public string Inschool { get; set; }
        public string PatientType { get; set; }
        public string PopulationType { get; set; }
        public string KeyPopulationType { get; set; }
        public string PatientResidentCounty { get; set; }
        public string PatientResidentSubCounty { get; set; }
        public string PatientResidentLocation { get; set; }
        public string PatientResidentSubLocation { get; set; }
        public string PatientResidentWard { get; set; }
        public string PatientResidentVillage { get; set; }
        public DateTime? TransferInDate { get; set; }
        public int PatientPID { get; set; }
        public string PatientCccNumber { get; set; }
        public Guid FacilityId { get; set; }
        public Guid? CurrentPatientId  { get; set; }
        public Guid? LiveSession { get; set; }
        public LiveStage LiveStage { get; set; }

        public void Standardize(PatientSourceBag patientSourceBag)
        {
            LiveSession = patientSourceBag.ManifestId;
            FacilityId = patientSourceBag.FacilityId.Value;
        }
    }
}
