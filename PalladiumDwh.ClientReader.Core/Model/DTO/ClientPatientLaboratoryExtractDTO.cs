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
        public int PatientPID { get; set; }
        public string PatientCccNumber { get; set; }
        public int FacilityId { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }


        public ClientPatientLaboratoryExtractDTO()
        {
        }

        public ClientPatientLaboratoryExtractDTO(int? visitId, DateTime? orderedByDate, DateTime? reportedByDate, string testName, int? enrollmentTest, string testResult, int patientPid, string patientCccNumber, int facilityId, string emr, string project)
        {
            VisitId = visitId;
            OrderedByDate = orderedByDate;
            ReportedByDate = reportedByDate;
            TestName = testName;
            EnrollmentTest = enrollmentTest;
            TestResult = testResult;
            PatientPID = patientPid;
            PatientCccNumber = patientCccNumber;
            FacilityId = facilityId;
            Emr = emr;
            Project = project;
        }

        public ClientPatientLaboratoryExtractDTO(ClientPatientLaboratoryExtract extract)
        {
            PatientPID = extract.PatientPK; //TODO PatientPID = extract.PatientPK;
            PatientCccNumber = extract.PatientID; //TODO PatientCccNumber = extract.PatientID;
            FacilityId = extract.SiteCode; //TODO FacilityId = extract.SiteCode
            VisitId = extract.VisitId;
            OrderedByDate = extract.OrderedByDate;
            ReportedByDate = extract.ReportedByDate;
            TestName = extract.TestName;
            EnrollmentTest = extract.EnrollmentTest;
            TestResult = extract.TestResult;

            Emr = extract.Emr;
            Project = extract.Project;



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

        public DateTime? DateSampleTaken { get; set; }
        public string SampleType { get; set; }
    }
}
