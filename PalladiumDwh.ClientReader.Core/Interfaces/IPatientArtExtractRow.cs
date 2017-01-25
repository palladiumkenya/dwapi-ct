using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IPatientArtExtractRow: IExtractRow
    {
        int PatientPK { get; set; }
        string PatientID { get; set; }
        int FacilityId { get; set; }
        int SiteCode { get; set; }
        string FacilityName { get; set; }
        DateTime DOB { get; set; }
        decimal AgeEnrollment { get; set; }
        decimal AgeARTStart { get; set; }
        decimal AgeLastVisit { get; set; }
        DateTime RegistrationDate { get; set; }
        string Gender { get; set; }
        DateTime StartARTDate { get; set; }
        DateTime PreviousARTStartDate { get; set; }
        string PreviousARTRegimen { get; set; }
        DateTime StartARTAtThisFacility { get; set; }
        string StartRegimen { get; set; }
        string StartRegimenLine { get; set; }
        DateTime LastARTDate { get; set; }
        string LastRegimen { get; set; }
        string LastRegimenLine { get; set; }
        decimal Duration { get; set; }
        DateTime ExpectedReturn { get; set; }
        string Provider { get; set; }
        DateTime LastVisit { get; set; }
        string ExitReason { get; set; }
        DateTime ExitDate { get; set; }
    }
}