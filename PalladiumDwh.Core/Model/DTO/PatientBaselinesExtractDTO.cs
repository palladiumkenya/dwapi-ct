using System;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Model.DTO
{
    public class PatientBaselinesExtractDTO
    {
        public int? eCD4 { get; set; }
        public DateTime? eCD4Date { get; set; }
        public int? eWHO { get; set; }
        public DateTime? eWHODate { get; set; }
        public int? bCD4 { get; set; }
        public DateTime? bCD4Date { get; set; }
        public int? bWHO { get; set; }
        public DateTime? bWHODate { get; set; }
        public int? lastWHO { get; set; }
        public DateTime? lastWHODate { get; set; }
        public int? lastCD4 { get; set; }
        public DateTime? lastCD4Date { get; set; }
        public int? m12CD4 { get; set; }
        public DateTime? m12CD4Date { get; set; }
        public int? m6CD4 { get; set; }
        public DateTime? m6CD4Date { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }

        /*
        public PatientBaselinesExtractDTO(PatientBaselinesExtractDTO patientBaselinesExtract)
        {
            
            eCD4 = patientBaselinesExtract.eCd4;
            eCD4Date = eCd4Date;
            eWHO = eWho;
            eWHODate = eWhoDate;
            bCD4 = bCd4;
            bCD4Date = bCd4Date;
            bWHO = bWho;
            bWHODate = bWhoDate;
            lastWHO = lastWho;
            lastWHODate = lastWhoDate;
            lastCD4 = lastCd4;
            lastCD4Date = lastCd4Date;
            m12CD4 = m12Cd4;
            m12CD4Date = m12Cd4Date;
            m6CD4 = m6Cd4;
            m6CD4Date = m6Cd4Date;
            Emr = emr;
            Project = project;
            PatientId = patientId;
        }
        */
        public  PatientBaselinesExtract GeneratePatientBaselinesExtract()
        {
            return new PatientBaselinesExtract( 
                eCD4 ,eCD4Date,eWHO ,eWHODate ,bCD4,bCD4Date,bWHO,bWHODate,lastWHO,
                lastWHODate ,lastCD4 ,lastCD4Date ,m12CD4 ,m12CD4Date ,m6CD4 ,m6CD4Date ,Emr,Project,PatientId);
        }
    }
}
