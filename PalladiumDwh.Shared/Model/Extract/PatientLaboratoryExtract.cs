using System;
using PalladiumDwh.Shared.Custom;
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
        public string Reason { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }
        public bool Voided { get; set; }


        public PatientLaboratoryExtract()
        {
            Created = DateTime.Now;
        }

        public PatientLaboratoryExtract(int? visitId, DateTime? orderedByDate, DateTime? reportedByDate, string testName, int? enrollmentTest, string testResult, Guid patientId, string emr, string project,
            DateTime? dateSampleTaken,string sampleType, string reason ,DateTime? date_Created,DateTime? date_Last_Modified,string recordUUID,bool voided
            )
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
RecordUUID = recordUUID;
            Voided = voided;


            DateSampleTaken = dateSampleTaken;
            SampleType = sampleType;
            Reason = reason;
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
            this.StandardizeExtract();
        }
    }
}
