using System;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Model
{
    public  class PatientLaboratoryExtract:Entity
    {
        public int? VisitId { get; set; }
        public DateTime? OrderedByDate { get; set; }
        public DateTime? ReportedByDate { get; set; }
        public string TestName { get; set; }
        public string TestResult { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }

        public PatientLaboratoryExtract()
        {
        }

        public PatientLaboratoryExtract(int? visitId, DateTime? orderedByDate, DateTime? reportedByDate, string testName, string testResult, string emr, string project, Guid patientId)
        {
            VisitId = visitId;
            OrderedByDate = orderedByDate;
            ReportedByDate = reportedByDate;
            TestName = testName;
            TestResult = testResult;
            Emr = emr;
            Project = project;
            PatientId = patientId;
        }
    }
}
