using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.ClientReader.Core.Interfaces.DTOs;

namespace PalladiumDwh.ClientReader.Core.Model.DTO
{
    public  class ClientPatientLaboratoryExtractDTO : IClientPatientLaboratoryExtractDTO
    {
        public int? VisitId { get; set; }
        public DateTime? OrderedByDate { get; set; }
        public DateTime? ReportedByDate { get; set; }
        public string TestName { get; set; }
        public int? EnrollmentTest { get; set; }
        public string TestResult { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        

        public ClientPatientLaboratoryExtractDTO()
        {
        }

        public ClientPatientLaboratoryExtractDTO(int? visitId, DateTime? orderedByDate, DateTime? reportedByDate, string testName, int? enrollmentTest, string testResult, string emr, string project, Guid patientId)
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
        }


        public ClientPatientLaboratoryExtractDTO(ClientPatientLaboratoryExtract patientLaboratoryExtract)
        {
            VisitId = patientLaboratoryExtract.VisitId;
            OrderedByDate = patientLaboratoryExtract.OrderedByDate;
            ReportedByDate = patientLaboratoryExtract.ReportedByDate;
            TestName = patientLaboratoryExtract.TestName;
            EnrollmentTest = patientLaboratoryExtract.EnrollmentTest;
            TestResult = patientLaboratoryExtract.TestResult;
            Emr = patientLaboratoryExtract.Emr;
            Project = patientLaboratoryExtract.Project;
            //PatientId = patientLaboratoryExtract.PatientId;
        }

      
        public IEnumerable<ClientPatientLaboratoryExtractDTO> GenerateLaboratoryExtractDtOs(IEnumerable<ClientPatientLaboratoryExtract> extracts)
        {
            var laboratoryExtractDtos = new List<ClientPatientLaboratoryExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                laboratoryExtractDtos.Add(new ClientPatientLaboratoryExtractDTO(e));
            }
            return laboratoryExtractDtos;
        }

        public ClientPatientLaboratoryExtract GeneratePatientLaboratoryExtract(Guid patientId)
        {
            PatientId = patientId;
            return new ClientPatientLaboratoryExtract();
//                VisitId, OrderedByDate, ReportedByDate, TestName,EnrollmentTest, TestResult, PatientId, Emr, Project);
        }
    }
}
