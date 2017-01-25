using System;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Extracts
{
    public interface IPatientBaselinesExtractRow: IExtractRow
    {
        int PatientPK { get; set; }
        string PatientID { get; set; }
        int FacilityId { get; set; }
        int SiteCode { get; set; }
        int bCD4 { get; set; }
        DateTime bCD4Date { get; set; }
        int bWAB { get; set; }
        DateTime bWABDate { get; set; }
        int bWHO { get; set; }
        DateTime bWHODate { get; set; }
        int eWAB { get; set; }
        DateTime eWABDate { get; set; }
        int eCD4 { get; set; }
        DateTime eCD4Date { get; set; }
        int eWHO { get; set; }
        DateTime eWHODate { get; set; }
        int lastWHO { get; set; }
        DateTime lastWHODate { get; set; }
        int lastCD4 { get; set; }
        DateTime lastCD4Date { get; set; }
        int m12CD4 { get; set; }
        DateTime m12CD4Date { get; set; }
        int m6CD4 { get; set; }
        DateTime m6CD4Date { get; set; }
    }
}