using System;

namespace PalladiumDwh.Shared.Interfaces
{
    public interface IPatient:IPatientNew
    {

        string Gender { get; set; }
        DateTime? DOB { get; set; }
        DateTime? RegistrationDate { get; set; }
        DateTime? RegistrationAtCCC { get; set; }
        DateTime? RegistrationATPMTCT { get; set; }
        DateTime? RegistrationAtTBClinic { get; set; }
        string Region { get; set; }
        string PatientSource { get; set; }
        string District { get; set; }
        string Village { get; set; }
        string ContactRelation { get; set; }
        DateTime? LastVisit { get; set; }
        string MaritalStatus { get; set; }
        string EducationLevel { get; set; }
        DateTime? DateConfirmedHIVPositive { get; set; }
        string PreviousARTExposure { get; set; }
        DateTime? PreviousARTStartDate { get; set; }
        string StatusAtCCC { get; set; }
        string StatusAtPMTCT { get; set; }
        string StatusAtTBClinic { get; set; }
        string Orphan { get; set; }
        string Inschool { get; set; }
        string PatientType { get; set; }
        string PopulationType { get; set; }
        string KeyPopulationType { get; set; }
        string PatientResidentCounty { get; set; }
        string PatientResidentSubCounty { get; set; }
        string PatientResidentLocation { get; set; }
        string PatientResidentSubLocation { get; set; }
        string PatientResidentWard { get; set; }
        string PatientResidentVillage { get; set; }
        DateTime? TransferInDate { get; set; }
        DateTime? Date_Created { get; set; } 
        DateTime? Date_Last_Modified { get; set; } 
    }
}

