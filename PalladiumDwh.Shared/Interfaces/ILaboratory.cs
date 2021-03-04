using System;

namespace PalladiumDwh.Shared.Interfaces
{
    public interface ILaboratory :ILabNew
    {
        int? VisitId { get; set; }
        DateTime? OrderedByDate { get; set; }
        DateTime? ReportedByDate { get; set; }
        string TestName { get; set; }
        int? EnrollmentTest { get; set; }
        string TestResult { get; set; }
    }
}
