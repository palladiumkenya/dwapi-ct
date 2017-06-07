using System;

namespace PalladiumDwh.Shared.Interfaces
{
    public interface IPatient
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
      
    }
}