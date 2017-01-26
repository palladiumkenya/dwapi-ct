using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class PatientBaselinesExtractDTO : IPatientBaselinesExtractDTO
    {

        public int? bCD4 { get; set; }
        public DateTime? bCD4Date { get; set; }
        public int? bWAB { get; set; }
        public DateTime? bWABDate { get; set; }
        public int? bWHO { get; set; }
        public DateTime? bWHODate { get; set; }
        public int? eWAB { get; set; }
        public DateTime? eWABDate { get; set; }
        public int? eCD4 { get; set; }
        public DateTime? eCD4Date { get; set; }
        public int? eWHO { get; set; }
        public DateTime? eWHODate { get; set; }
        public int? lastWHO { get; set; }
        public DateTime? lastWHODate { get; set; }
        public int? lastCD4 { get; set; }
        public DateTime? lastCD4Date { get; set; }
        public int? lastWAB { get; set; }
        public DateTime? lastWABDate { get; set; }
        public int? m12CD4 { get; set; }
        public DateTime? m12CD4Date { get; set; }
        public int? m6CD4 { get; set; }
        public DateTime? m6CD4Date { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }

        public PatientBaselinesExtractDTO()
        {
        }

        public PatientBaselinesExtractDTO(int? bCd4, DateTime? bCd4Date, int? bWab, DateTime? bWabDate, int? bWho, DateTime? bWhoDate, int? eWab, DateTime? eWabDate, int? eCd4, DateTime? eCd4Date, int? eWho, DateTime? eWhoDate, int? lastWho, DateTime? lastWhoDate, int? lastCd4, DateTime? lastCd4Date, int? lastWab, DateTime? lastWabDate, int? m12Cd4, DateTime? m12Cd4Date, int? m6Cd4, DateTime? m6Cd4Date, string emr, string project, Guid patientId)
        {
            bCD4 = bCd4;
            bCD4Date = bCd4Date;
            bWAB = bWab;
            bWABDate = bWabDate;
            bWHO = bWho;
            bWHODate = bWhoDate;
            eWAB = eWab;
            eWABDate = eWabDate;
            eCD4 = eCd4;
            eCD4Date = eCd4Date;
            eWHO = eWho;
            eWHODate = eWhoDate;
            lastWHO = lastWho;
            lastWHODate = lastWhoDate;
            lastCD4 = lastCd4;
            lastCD4Date = lastCd4Date;
            lastWAB = lastWab;
            lastWABDate = lastWabDate;
            m12CD4 = m12Cd4;
            m12CD4Date = m12Cd4Date;
            m6CD4 = m6Cd4;
            m6CD4Date = m6Cd4Date;
            Emr = emr;
            Project = project;
            PatientId = patientId;
        }

        public PatientBaselinesExtractDTO(PatientBaselinesExtract patientBaselinesExtract)
        {

            bCD4 = patientBaselinesExtract.bCD4;
            bCD4Date = patientBaselinesExtract.bCD4Date;
            bWAB = patientBaselinesExtract.bWAB;
            bWABDate = patientBaselinesExtract.bWABDate;
            bWHO = patientBaselinesExtract.bWHO;
            bWHODate = patientBaselinesExtract.bWHODate;
            eWAB = patientBaselinesExtract.eWAB;
            eWABDate = patientBaselinesExtract.eWABDate;
            eCD4 = patientBaselinesExtract.eCD4;
            eCD4Date = patientBaselinesExtract.eCD4Date;
            eWHO = patientBaselinesExtract.eWHO;
            eWHODate = patientBaselinesExtract.eWHODate;
            lastWHO = patientBaselinesExtract.lastWHO;
            lastWHODate = patientBaselinesExtract.lastWHODate;
            lastCD4 = patientBaselinesExtract.lastCD4;
            lastCD4Date = patientBaselinesExtract.lastCD4Date;
            lastWAB = patientBaselinesExtract.lastWAB;
            lastWABDate = patientBaselinesExtract.lastWABDate;
            m12CD4 = patientBaselinesExtract.m12CD4;
            m12CD4Date = patientBaselinesExtract.m12CD4Date;
            m6CD4 = patientBaselinesExtract.m6CD4;
            m6CD4Date = patientBaselinesExtract.m6CD4Date;
            Emr = patientBaselinesExtract.Emr;
            Project = patientBaselinesExtract.Project;
            PatientId = patientBaselinesExtract.PatientId;

        }



        public IEnumerable<PatientBaselinesExtractDTO> GeneratePatientBaselinesExtractDtOs(IEnumerable<PatientBaselinesExtract> extracts)
        {
            var baselinesExtractDtos = new List<PatientBaselinesExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                baselinesExtractDtos.Add(new PatientBaselinesExtractDTO(e));
            }
            return baselinesExtractDtos;
        }
        public  PatientBaselinesExtract GeneratePatientBaselinesExtract(Guid patientId)
        {
            PatientId = patientId;
            return new PatientBaselinesExtract(bCD4,bCD4Date,bWAB,bWABDate,bWHO,bWHODate,eWAB,eWABDate,eCD4,eCD4Date,eWHO,eWHODate,
                lastWHO,lastWHODate,lastCD4,lastCD4Date,lastWAB,lastWABDate,m12CD4,m12CD4Date,m6CD4,m6CD4Date,PatientId,Emr,Project
            );
        }
    }
}
