using System;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Model.DTO
{
    public  class PatientLaboratoryExtractDTO
    {
        public int? VisitId { get; set; }
        public DateTime? OrderedByDate { get; set; }
        public DateTime? ReportedByDate { get; set; }
        public string TestName { get; set; }
        public string TestResult { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }

        public PatientLaboratoryExtractDTO(PatientLaboratoryExtract patientLaboratoryExtract)
        {
            VisitId = patientLaboratoryExtract.VisitId;
            OrderedByDate = patientLaboratoryExtract.OrderedByDate;
            ReportedByDate = patientLaboratoryExtract.ReportedByDate;
            TestName = patientLaboratoryExtract.TestName;
            TestResult = patientLaboratoryExtract.TestResult;
            Emr = patientLaboratoryExtract.Emr;
            Project = patientLaboratoryExtract.Project;
            PatientId = patientLaboratoryExtract.PatientId;
        }

        public PatientLaboratoryExtract GeneratePatientLaboratoryExtract()
        {
            return new PatientLaboratoryExtract(VisitId, OrderedByDate, ReportedByDate, TestName, TestResult, Emr, Project, PatientId);
        }
    }
}
