using System;
using System.Collections.Generic;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Model
{
    public class PatientExtract:Entity
    {
        public int SiteCode { get; set; }
        public string PatientCccNumber { get; set; }
        public string FacilityName { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? RegistrationAtCCC { get; set; }
        public DateTime? RegistrationATPMTCT { get; set; }
        public DateTime? RegistrationAtTBClinic { get; set; }
        public string PatientSource { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string Village { get; set; }
        public string ContactRelation { get; set; }
        public DateTime? LastVisit { get; set; }
        public string MaritalStatus { get; set; }
        public string EducationLevel { get; set; }
        public DateTime? DateConfirmedHIVPositive { get; set; }
        public string PreviousARTExposure { get; set; }
        public DateTime? PreviousARTStartDate { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public int? Uploaded { get; set; }
    
        public virtual ICollection<PatientArtExtract> PatientArtExtracts { get; set; }=new List<PatientArtExtract>();
        public virtual ICollection<PatientBaselinesExtract> PatientBaselinesExtracts { get; set; }=new List<PatientBaselinesExtract>();
        public virtual ICollection<PatientLaboratoryExtract> PatientLaboratoryExtracts { get; set; }=new List<PatientLaboratoryExtract>();
        public virtual ICollection<PatientPharmacyExtract> PatientPharmacyExtracts { get; set; }=new List<PatientPharmacyExtract>();
        public virtual ICollection<PatientStatusExtract> PatientStatusExtracts { get; set; }=new List<PatientStatusExtract>();
        public virtual ICollection<PatientVisitExtract> PatientVisitExtracts { get; set; }=new List<PatientVisitExtract>();
    }
}
