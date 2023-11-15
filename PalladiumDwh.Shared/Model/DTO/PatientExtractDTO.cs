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
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid FacilityId { get; set; }

        public string Pkv { get; set; }
        public string Occupation { get; set; }
        public string NUPI { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }
        public bool Voided { get; set; }


        public PatientExtractDTO()
        {

        }

        public PatientExtractDTO(int patientPid, string patientCccNumber, string gender, DateTime? dob, DateTime? registrationDate, DateTime? registrationAtCcc, DateTime? registrationAtpmtct, DateTime? registrationAtTbClinic, string patientSource, string region, string district, string village, string contactRelation, DateTime? lastVisit, string maritalStatus, string educationLevel, DateTime? dateConfirmedHivPositive, string previousArtExposure, DateTime? previousArtStartDate, string statusAtCcc, string statusAtPmtct, string statusAtTbClinic, string emr, string project, Guid facilityId,
            string orphan, string inschool, string patientType, string populationType, string keyPopulationType, string patientResidentCounty, string patientResidentSubCounty, string patientResidentLocation, string patientResidentSubLocation, string patientResidentWard, string patientResidentVillage, DateTime? transferInDate, string nupi, DateTime? date_Created,DateTime? date_Last_Modified, string recordUUID, bool voided)
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

            Orphan = orphan;
            Inschool = inschool;
            PatientType = patientType;
            PopulationType = populationType;
            KeyPopulationType = keyPopulationType;
            PatientResidentCounty = patientResidentCounty;
            PatientResidentSubCounty = patientResidentSubCounty;
            PatientResidentLocation = patientResidentLocation;
            PatientResidentSubLocation = patientResidentSubLocation;
            PatientResidentWard = patientResidentWard;
            PatientResidentVillage = patientResidentVillage;
            TransferInDate = transferInDate;
            NUPI = nupi;
            Date_Created=date_Created;
            Date_Last_Modified=date_Last_Modified;
            RecordUUID=recordUUID;
            Voided = voided;
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

            Orphan = patient.Orphan;
            Inschool = patient.Inschool;
            PatientType = patient.PatientType;
            PopulationType = patient.PopulationType;
            KeyPopulationType = patient.KeyPopulationType;
            PatientResidentCounty = patient.PatientResidentCounty;
            PatientResidentSubCounty = patient.PatientResidentSubCounty;
            PatientResidentLocation = patient.PatientResidentLocation;
            PatientResidentSubLocation = patient.PatientResidentSubLocation;
            PatientResidentWard = patient.PatientResidentWard;
            PatientResidentVillage = patient.PatientResidentVillage;
            TransferInDate = patient.TransferInDate;

            Occupation = patient.Occupation;
            Pkv = patient.Pkv;
            NUPI = patient.NUPI;
            Date_Created=patient.Date_Created;
            Date_Last_Modified=patient.Date_Last_Modified;
            RecordUUID=patient.RecordUUID;
            Voided=patient.Voided;

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
                Project,
                Orphan,
                Inschool,
                PatientType,
                PopulationType,
                KeyPopulationType,
                PatientResidentCounty,
                PatientResidentSubCounty,
                PatientResidentLocation,
                PatientResidentSubLocation,
                PatientResidentWard,
                PatientResidentVillage,
                TransferInDate,
                Pkv,Occupation,
                NUPI, Date_Created, 
                Date_Last_Modified,
                RecordUUID,
                Voided
            );
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(PatientCccNumber);
        }


    }
}
