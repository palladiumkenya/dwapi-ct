using System;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Extracts
{
    public interface IPatientLaboratoryExtractRow:IExtractRow
    {
        int PatientPK { get; set; }
        string PatientID { get; set; }
        int FacilityId { get; set; }
        int SiteCode { get; set; }
        string FacilityName { get; set; }
        string SatelliteName { get; set; }
        int VisitId { get; set; }
        DateTime OrderedByDate { get; set; }
        DateTime ReportedByDate { get; set; }
        string TestName { get; set; }
        int EnrollmentTest { get; set; }
        string TestResult { get; set; }
    }
}