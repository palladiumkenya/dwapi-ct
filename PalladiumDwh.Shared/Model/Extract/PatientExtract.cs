using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
{
    public class PatientExtract:Entity, IPatientExtract
    {
        public int PatientPID { get; set; }
        public string PatientCccNumber { get; set; }
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
        public string StatusAtCCC { get; set; }
        public string StatusAtPMTCT { get; set; }
        public string StatusAtTBClinic { get; set; }
        public Guid FacilityId { get; set; }
        public DateTime? Created { get; set; }

        public virtual ICollection<PatientArtExtract> PatientArtExtracts { get; set; }=new List<PatientArtExtract>();
        public virtual ICollection<PatientBaselinesExtract> PatientBaselinesExtracts { get; set; }=new List<PatientBaselinesExtract>();
        public virtual ICollection<PatientLaboratoryExtract> PatientLaboratoryExtracts { get; set; }=new List<PatientLaboratoryExtract>();
        public virtual ICollection<PatientPharmacyExtract> PatientPharmacyExtracts { get; set; }=new List<PatientPharmacyExtract>();
        public virtual ICollection<PatientStatusExtract> PatientStatusExtracts { get; set; }=new List<PatientStatusExtract>();
        public virtual ICollection<PatientVisitExtract> PatientVisitExtracts { get; set; }=new List<PatientVisitExtract>();

        public PatientExtract()
        {
            Created = DateTime.Now;
        }

        public PatientExtract(int patientPid, string patientCccNumber, string gender, DateTime? dob, DateTime? registrationDate, DateTime? registrationAtCcc, DateTime? registrationAtpmtct, DateTime? registrationAtTbClinic, string patientSource, string region, string district, string village, string contactRelation, DateTime? lastVisit, string maritalStatus, string educationLevel, DateTime? dateConfirmedHivPositive, string previousArtExposure, DateTime? previousArtStartDate, string statusAtCcc, string statusAtPmtct, string statusAtTbClinic, Guid facilityId,string emr,string project)
        {
            PatientPID = patientPid;
            PatientCccNumber = patientCccNumber;
            Gender = gender;
            DOB = dob;
            RegistrationDate = registrationDate;
            RegistrationAtCCC = registrationAtCcc;
            RegistrationATPMTCT = registrationAtpmtct;
            RegistrationAtTBClinic = registrationAtTbClinic;
            PatientSource = patientSource;
            Region = region;
            District = district;
            Village = village;
            ContactRelation = contactRelation;
            LastVisit = lastVisit;
            MaritalStatus = maritalStatus;
            EducationLevel = educationLevel;
            DateConfirmedHIVPositive = dateConfirmedHivPositive;
            PreviousARTExposure = previousArtExposure;
            PreviousARTStartDate = previousArtStartDate;
            StatusAtCCC = statusAtCcc;
            StatusAtPMTCT = statusAtPmtct;
            StatusAtTBClinic = statusAtTbClinic;
            FacilityId = facilityId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
        }

        public void AddPatientArtExtracts(IEnumerable<PatientArtExtract> extracts)
        {
            foreach (var e in extracts)
            {
                e.PatientId = Id;
                PatientArtExtracts.Add(e);
            }
        }
        public void AddPatientBaselinesExtracts(IEnumerable<PatientBaselinesExtract> extracts)
        {
            foreach (var e in extracts)
            {
                e.PatientId = Id;
                PatientBaselinesExtracts.Add(e);
            }
        }
        public void AddPatientLaboratoryExtracts(IEnumerable<PatientLaboratoryExtract> extracts)
        {
            foreach (var e in extracts)
            {
                e.PatientId = Id;
                PatientLaboratoryExtracts.Add(e);
            }
        }
        public void AddPatientPharmacyExtracts(IEnumerable<PatientPharmacyExtract> extracts)
        {
            foreach (var e in extracts)
            {
                e.PatientId = Id;
                PatientPharmacyExtracts.Add(e);
            }
        }
        public void AddPatientStatusExtracts(IEnumerable<PatientStatusExtract> extracts)
        {
            foreach (var e in extracts)
            {
                e.PatientId = Id;
                PatientStatusExtracts.Add(e);
            }
        }
        public void AddPatientVisitExtracts(IEnumerable<PatientVisitExtract> extracts)
        {
            foreach (var e in extracts)
            {
                e.PatientId = Id;
                PatientVisitExtracts.Add(e);
            }
        }
    }
}