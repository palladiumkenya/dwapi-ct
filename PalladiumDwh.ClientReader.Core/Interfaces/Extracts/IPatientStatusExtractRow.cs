using System;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Extracts
{
    public interface IPatientStatusExtractRow:IExtractRow
    {
        int PatientPK { get; set; }
        string PatientID { get; set; }
        int FacilityId { get; set; }
        int SiteCode { get; set; }
        string FacilityName { get; set; }
        string ExitDescription { get; set; }
        DateTime ExitDate { get; set; }
        string ExitReason { get; set; }
    }
}