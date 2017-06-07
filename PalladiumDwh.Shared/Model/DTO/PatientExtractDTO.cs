using System;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class PatientExtractDTO : IPatientExtractDTO
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
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid FacilityId { get; set; }

        public PatientExtractDTO()
        {

        }

        public PatientExtractDTO(int patientPid, string patientCccNumber, string gender, DateTime? dob, DateTime? registrationDate, DateTime? registrationAtCcc, DateTime? registrationAtpmtct, DateTime? registrationAtTbClinic, string patientSource, string region, string district, string village, string contactRelation, DateTime? lastVisit, string maritalStatus, string educationLevel, DateTime? dateConfirmedHivPositive, string previousArtExposure, DateTime? previousArtStartDate, string statusAtCcc, string statusAtPmtct, string statusAtTbClinic, string emr, string project, Guid facilityId)
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
        }

        public PatientExtractDTO(PatientExtract patient)
        {
            PatientPID = patient.PatientPID;
            PatientCccNumber = patient.PatientCccNumber;
            Gender = patient.Gender;
            DOB = patient.DOB;
            RegistrationDate = patient.RegistrationDate;
            RegistrationAtCCC = patient.RegistrationAtCCC;
            RegistrationATPMTCT = patient.RegistrationATPMTCT;
            RegistrationAtTBClinic = patient.RegistrationAtTBClinic;
            PatientSource = patient.PatientSource;
            Region = patient.Region;
            District = patient.District;
            Village = patient.Village;
            ContactRelation = patient.ContactRelation;
            LastVisit = patient.LastVisit;
            MaritalStatus = patient.MaritalStatus;
            EducationLevel = patient.EducationLevel;
            DateConfirmedHIVPositive = patient.DateConfirmedHIVPositive;
            PreviousARTExposure = patient.PreviousARTExposure;
            PreviousARTStartDate = patient.PreviousARTStartDate;
            StatusAtCCC=patient.StatusAtCCC;
            StatusAtPMTCT=patient.StatusAtPMTCT;
            StatusAtTBClinic =patient.StatusAtTBClinic;
            FacilityId = patient.FacilityId;
            Emr = patient.Emr;
            Project = patient.Project;
        }

        public PatientExtract GeneratePatient(Guid facilityId)
        {
            FacilityId = facilityId;

            return new PatientExtract(
                PatientPID,
                PatientCccNumber, 
                Gender, 
                DOB, 
                RegistrationDate, 
                RegistrationAtCCC, 
                RegistrationATPMTCT,
                RegistrationAtTBClinic,
                PatientSource, 
                Region, 
                District, 
                Village, 
                ContactRelation, 
                LastVisit, 
                MaritalStatus, 
                EducationLevel,
                DateConfirmedHIVPositive, 
                PreviousARTExposure, 
                PreviousARTStartDate, 
                StatusAtCCC, 
                StatusAtPMTCT,
                StatusAtTBClinic, 
                FacilityId,
                Emr, 
                Project
            );
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(PatientCccNumber);
        }

     
      
    }
}
