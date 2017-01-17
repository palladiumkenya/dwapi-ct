using System;
using System.Collections.Generic;
using System.Linq;

namespace PalladiumDwh.DWapi.Client.Model.DTO
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
