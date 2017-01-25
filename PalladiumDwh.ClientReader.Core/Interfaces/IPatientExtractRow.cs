using System;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IPatientExtractRow: IExtractRow
    {
        string PatientID { get; set; }
        int PatientPK { get; set; }
        int FacilityId { get; set; }
        int SiteCode { get; set; }
        string FacilityName { get; set; }
        string SatelliteName { get; set; }
        string Gender { get; set; }
        DateTime DOB { get; set; }
        DateTime RegistrationDate { get; set; }
        DateTime RegistrationAtCCC { get; set; }
        DateTime RegistrationATPMTCT { get; set; }
        DateTime RegistrationAtTBClinic { get; set; }
        string Region { get; set; }
        string PatientSource { get; set; }
        string District { get; set; }
        string Village { get; set; }
        string ContactRelation { get; set; }
        DateTime LastVisit { get; set; }
        string MaritalStatus { get; set; }
        string EducationLevel { get; set; }
        DateTime DateConfirmedHIVPositive { get; set; }
        string PreviousARTExposure { get; set; }
        DateTime PreviousARTStartDate { get; set; }
        string StatusAtCCC { get; set; }
        string StatusAtPMTCT { get; set; }
        string StatusAtTBClinic { get; set; }
        string Emr { get; set; }
        string Project { get; set; }
    }
}