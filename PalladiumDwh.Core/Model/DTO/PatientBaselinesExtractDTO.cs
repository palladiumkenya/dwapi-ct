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

        
        public PatientBaselinesExtractDTO(PatientBaselinesExtractDTO patientBaselinesExtract)
        {
            
            eCD4 = patientBaselinesExtract.eCD4;
            eCD4Date = patientBaselinesExtract.eCD4Date;
            eWHO = patientBaselinesExtract.eWHO;
            eWHODate = patientBaselinesExtract.eWHODate;
            bCD4 = patientBaselinesExtract.bCD4;
            bCD4Date = patientBaselinesExtract.bCD4Date;
            bWHO = patientBaselinesExtract.bWHO;
            bWHODate = patientBaselinesExtract.bWHODate;
            lastWHO = patientBaselinesExtract.lastWHO;
            lastWHODate = patientBaselinesExtract.lastWHODate;
            lastCD4 = patientBaselinesExtract.lastCD4;
            lastCD4Date = patientBaselinesExtract.lastCD4Date;
            m12CD4 = patientBaselinesExtract.m6CD4;
            m12CD4Date = patientBaselinesExtract.m6CD4Date;
            m6CD4 = patientBaselinesExtract.m6CD4;
            m6CD4Date = patientBaselinesExtract.m6CD4Date;
            Emr = patientBaselinesExtract.Emr;
            Project = patientBaselinesExtract.Project;
            PatientId = patientBaselinesExtract.PatientId;
        }
        
        public  PatientBaselinesExtract GeneratePatientBaselinesExtract()
        {
            return new PatientBaselinesExtract( 
                eCD4 ,eCD4Date,eWHO ,eWHODate ,bCD4,bCD4Date,bWHO,bWHODate,lastWHO,
                lastWHODate ,lastCD4 ,lastCD4Date ,m12CD4 ,m12CD4Date ,m6CD4 ,m6CD4Date ,Emr,Project,PatientId);
        }
    }
}
