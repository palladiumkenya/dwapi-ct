using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Core.Model
{
    [Table("PatientLaboratoryExtract")]
    public  class ClientPatientLaboratoryExtract: ClientExtract, IClientPatientLaboratoryExtract
    {
        [Key]
        public override Guid Id { get; set; }
        public int? VisitId { get; set; }
        public DateTime? OrderedByDate { get; set; }
        public DateTime? ReportedByDate { get; set; }
        public string TestName { get; set; }
        public int? EnrollmentTest { get; set; }
        public string TestResult { get; set; }

        public ClientPatientLaboratoryExtract()
        {
        }

        public ClientPatientLaboratoryExtract(int? visitId, DateTime? orderedByDate, DateTime? reportedByDate, string testName, int? enrollmentTest, string testResult, string emr, string project)
        {
            VisitId = visitId;
            OrderedByDate = orderedByDate;
            ReportedByDate = reportedByDate;
            TestName = testName;
            EnrollmentTest = enrollmentTest;
            TestResult = testResult;
            Emr = emr;
            Project = project;
        }

        public ClientPatientLaboratoryExtract(TempPatientLaboratoryExtract extract)
        {
            VisitId = extract.VisitId;
            OrderedByDate = extract.OrderedByDate;
            ReportedByDate = extract.ReportedByDate;
            TestName = extract.TestName;
            EnrollmentTest = extract.EnrollmentTest;
            TestResult = extract.TestResult;
            //:TODO Add to Lab
            //Emr = extract.Emr;
            //Project = extract.Project;

        }
    }
}
