using System;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
{
    public  class PatientLaboratoryExtract:Entity, IPatientLaboratoryExtract
    {
        public int? VisitId { get; set; }
        public DateTime? OrderedByDate { get; set; }
        public DateTime? ReportedByDate { get; set; }
        public string TestName { get; set; }
        public int? EnrollmentTest { get; set; }
        public string TestResult { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? DateSampleTaken { get; set; }
        public string SampleType { get; set; }

        public PatientLaboratoryExtract()
        {
            Created = DateTime.Now;
        }

        public PatientLaboratoryExtract(int? visitId, DateTime? orderedByDate, DateTime? reportedByDate, string testName, int? enrollmentTest, string testResult, Guid patientId, string emr, string project)
        {
            VisitId = visitId;
            OrderedByDate = orderedByDate;
            ReportedByDate = reportedByDate;
            TestName = testName;
            EnrollmentTest = enrollmentTest;
            TestResult = testResult;
            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
        }


    }
}
