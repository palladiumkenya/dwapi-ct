using System;

namespace PalladiumDwh.Shared.Interfaces
{
    public interface IPatientExtractDTO
    {
        string ContactRelation { get; set; }
        DateTime? DateConfirmedHIVPositive { get; set; }
        string District { get; set; }
        DateTime? DOB { get; set; }
        string EducationLevel { get; set; }
        string Emr { get; set; }
        Guid FacilityId { get; set; }
        string Gender { get; set; }
        DateTime? LastVisit { get; set; }
        string MaritalStatus { get; set; }
        string PatientCccNumber { get; set; }
        int PatientPID { get; set; }
        string PatientSource { get; set; }
        string PreviousARTExposure { get; set; }
        DateTime? PreviousARTStartDate { get; set; }
        string Project { get; set; }
        string Region { get; set; }
        DateTime? RegistrationAtCCC { get; set; }
        DateTime? RegistrationATPMTCT { get; set; }
        DateTime? RegistrationAtTBClinic { get; set; }
        DateTime? RegistrationDate { get; set; }
        string Village { get; set; }
    }
}