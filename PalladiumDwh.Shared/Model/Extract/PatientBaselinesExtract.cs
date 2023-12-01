using System;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
{
    public class PatientBaselinesExtract : Entity, IPatientBaselinesExtract
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
        public Guid PatientId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }
        public bool Voided { get; set; }


        public PatientBaselinesExtract()
        {
            Created = DateTime.Now;
        }

        

        public PatientBaselinesExtract(int? bCd4, DateTime? bCd4Date, int? bWab, DateTime? bWabDate, int? bWho, DateTime? bWhoDate, int? eWab, DateTime? eWabDate, int? eCd4, DateTime? eCd4Date, int? eWho, DateTime? eWhoDate, int? lastWho, DateTime? lastWhoDate, int? lastCd4, DateTime? lastCd4Date, int? lastWab, DateTime? lastWabDate, int? m12Cd4, DateTime? m12Cd4Date, int? m6Cd4, DateTime? m6Cd4Date, Guid patientId, string emr, string project, DateTime? date_Created,DateTime? date_Last_Modified,string recordUUID,bool voided)
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
RecordUUID = recordUUID;
            Voided = voided;

            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
            this.StandardizeExtract();
        }
    }
}
