using System;
using PalladiumDwh.ClientReader.Core.Interfaces.DTOs;

namespace PalladiumDwh.ClientReader.Core.Model.DTO
{
    public class ClientPatientExtractDTO : IClientPatientExtractDTO
    {
        public int PatientPID { get; set; }
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
        public int FacilityId { get; set; }

        
        
        public ClientPatientExtractDTO()
        {
        }

        public ClientPatientExtractDTO(ClientPatientExtract extract)
        {
            PatientPID = extract.PatientPK;//TODO PatientPID = extract.PatientPK
            PatientCccNumber = extract.PatientID;//TODO PatientCccNumber = extract.PatientID
            FacilityName = extract.FacilityName;
            Gender = extract.Gender;
            DOB = extract.DOB;
            RegistrationDate = extract.RegistrationDate;
            RegistrationAtCCC = extract.RegistrationAtCCC;
            RegistrationATPMTCT = extract.RegistrationATPMTCT;
            RegistrationAtTBClinic = extract.RegistrationAtTBClinic;
            PatientSource = extract.PatientSource;
            Region = extract.Region;
            District = extract.District;
            Village = extract.Village;
            ContactRelation = extract.ContactRelation;
            LastVisit = extract.LastVisit;
            MaritalStatus = extract.MaritalStatus;
            EducationLevel = extract.EducationLevel;
            DateConfirmedHIVPositive = extract.DateConfirmedHIVPositive;
            PreviousARTExposure = extract.PreviousARTExposure;
            PreviousARTStartDate = extract.PreviousARTStartDate;
            StatusAtCCC = extract.StatusAtCCC;
            StatusAtPMTCT = extract.StatusAtPMTCT;
            StatusAtTBClinic = extract.StatusAtTBClinic;
            Emr = extract.Emr;
            Project = extract.Project;
            FacilityId = extract.SiteCode; //TODO FacilityId = extract.SiteCode
            NUPI = extract.NUPI;
            Date_Created = extract.Date_Created;
            Date_Last_Modified = extract.Date_Last_Modified;
            RecordUUID = extract.RecordUUID;

        }

        public string Pkv { get; set; }
        public string Occupation { get; set; }
        public string NUPI { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }

        
    }
}
