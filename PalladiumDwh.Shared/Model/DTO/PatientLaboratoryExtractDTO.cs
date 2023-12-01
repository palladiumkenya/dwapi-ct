using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public  class PatientLaboratoryExtractDTO : IPatientLaboratoryExtractDTO
    {
        public int? VisitId { get; set; }
        public DateTime? OrderedByDate { get; set; }
        public DateTime? ReportedByDate { get; set; }
        public string TestName { get; set; }
        public int? EnrollmentTest { get; set; }
        public string TestResult { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? DateSampleTaken { get; set; }
        public string SampleType { get; set; }
        public string Reason { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }
        public bool Voided { get; set; }


        public PatientLaboratoryExtractDTO()
        {
        }

        public PatientLaboratoryExtractDTO(int? visitId, DateTime? orderedByDate, DateTime? reportedByDate, string testName, int? enrollmentTest, string testResult, string emr, string project, Guid patientId, DateTime? date_Created,DateTime? date_Last_Modified, string recordUUID, bool voided)
        {
            VisitId = visitId;
            OrderedByDate = orderedByDate;
            ReportedByDate = reportedByDate;
            TestName = testName;
            EnrollmentTest = enrollmentTest;
            TestResult = testResult;
            Emr = emr;
            Project = project;
            PatientId = patientId;
            Date_Created=date_Created;
            Date_Last_Modified=date_Last_Modified;
            RecordUUID=recordUUID;

        }


        public PatientLaboratoryExtractDTO(PatientLaboratoryExtract patientLaboratoryExtract)
        {
            VisitId = patientLaboratoryExtract.VisitId;
            OrderedByDate = patientLaboratoryExtract.OrderedByDate;
            ReportedByDate = patientLaboratoryExtract.ReportedByDate;
            TestName = patientLaboratoryExtract.TestName;
            EnrollmentTest = patientLaboratoryExtract.EnrollmentTest;
            TestResult = patientLaboratoryExtract.TestResult;
            Emr = patientLaboratoryExtract.Emr;
            Project = patientLaboratoryExtract.Project;
            PatientId = patientLaboratoryExtract.PatientId;
            DateSampleTaken = patientLaboratoryExtract.DateSampleTaken;
            Reason = patientLaboratoryExtract.Reason;
            SampleType = patientLaboratoryExtract.SampleType;
            Date_Created=patientLaboratoryExtract.Date_Created;
            Date_Last_Modified=patientLaboratoryExtract.Date_Last_Modified;
            RecordUUID=patientLaboratoryExtract.RecordUUID;

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
            return new PatientLaboratoryExtract(VisitId, OrderedByDate, ReportedByDate, TestName,EnrollmentTest, TestResult, PatientId, Emr, Project,DateSampleTaken,SampleType, Reason, Date_Created, Date_Last_Modified,RecordUUID,
                Voided);
        }


    }
}
