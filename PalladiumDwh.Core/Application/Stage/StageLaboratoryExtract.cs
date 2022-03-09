using System;
using PalladiumDwh.Shared.Interfaces.Stages;

namespace PalladiumDwh.Core.Model
{
    public class StageLaboratoryExtract:StageExtract, IStageLaboratoryExtract
    {
        public DateTime? DateSampleTaken { get; set; }
        public string SampleType { get; set; }
        public int? VisitId { get; set; }
        public DateTime? OrderedByDate { get; set; }
        public DateTime? ReportedByDate { get; set; }
        public string TestName { get; set; }
        public int? EnrollmentTest { get; set; }
        public string TestResult { get; set; }
    }
}