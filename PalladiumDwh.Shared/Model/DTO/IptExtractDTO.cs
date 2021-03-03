using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class IptExtractDTO : IIptExtractDTO
    {
        // public string ExitDescription { get; set; }
        // public DateTime? ExitDate { get; set; }
        // public string ExitReason { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public IptExtractDTO()
        {
        }

        public IptExtractDTO(string exitDescription, DateTime? exitDate, string exitReason, string emr, string project, Guid patientId)
        {
            // ExitDescription = exitDescription;
            // ExitDate = exitDate;
            // ExitReason = exitReason;
            Emr = emr;
            Project = project;
            PatientId = patientId;
        }

        public IptExtractDTO(IptExtract IptExtract)
        {
            // ExitDescription = IptExtract.ExitDescription;
            // ExitDate = IptExtract.ExitDate;
            // ExitReason = IptExtract.ExitReason;
            Emr = IptExtract.Emr;
            Project = IptExtract.Project;
            PatientId = IptExtract.PatientId;
        }



        public IEnumerable<IptExtractDTO> GenerateIptExtractDtOs(IEnumerable<IptExtract> extracts)
        {
            var statusExtractDtos = new List<IptExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new IptExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public IptExtract GenerateIptExtract(Guid patientId)
        {
            PatientId = patientId;
            // return new IptExtract(ExitDescription, ExitDate, ExitReason, PatientId, Emr, Project);
            return new IptExtract();
        }


        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string OnTBDrugs { get; set; }
        public string OnIPT { get; set; }
        public string EverOnIPT { get; set; }
        public string Cough { get; set; }
        public string Fever { get; set; }
        public string NoticeableWeightLoss { get; set; }
        public string NightSweats { get; set; }
        public string Lethargy { get; set; }
        public string ICFActionTaken { get; set; }
        public string TestResult { get; set; }
        public string TBClinicalDiagnosis { get; set; }
        public string ContactsInvited { get; set; }
        public string EvaluatedForIPT { get; set; }
        public string StartAntiTBs { get; set; }
        public DateTime? TBRxStartDate { get; set; }
        public string TBScreening { get; set; }
        public string IPTClientWorkUp { get; set; }
        public string StartIPT { get; set; }
        public string IndicationForIPT { get; set; }
    }
}
