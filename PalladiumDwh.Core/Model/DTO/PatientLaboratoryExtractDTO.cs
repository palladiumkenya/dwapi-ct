using System;
using System.Collections.Generic;
using System.Linq;
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

        public PatientLaboratoryExtractDTO()
        {
        }

        public PatientLaboratoryExtractDTO(int? visitId, DateTime? orderedByDate, DateTime? reportedByDate, string testName, string testResult, string emr, string project, Guid patientId)
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

        public IEnumerable<PatientLaboratoryExtractDTO> GenerateLaboratoryExtractDtOs(IEnumerable<PatientLaboratoryExtract> extracts)
        {
            var laboratoryExtractDtos = new List<PatientLaboratoryExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                laboratoryExtractDtos.Add(new PatientLaboratoryExtractDTO(e));
            }
            return laboratoryExtractDtos;
        }

        public PatientLaboratoryExtract GeneratePatientLaboratoryExtract(Guid patientId)
        {
            PatientId = patientId;
            return new PatientLaboratoryExtract(VisitId, OrderedByDate, ReportedByDate, TestName, TestResult, Emr, Project, PatientId);
        }
    }
}
