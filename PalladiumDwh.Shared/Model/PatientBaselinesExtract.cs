using System;

namespace PalladiumDwh.Shared.Model
{
    public class PatientBaselinesExtract : Entity
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
        public Guid PatientId { get; set; }

        public PatientBaselinesExtract()
        {
        }

        public PatientBaselinesExtract(int? eCd4, DateTime? eCd4Date, int? eWho, DateTime? eWhoDate, int? bCd4, DateTime? bCd4Date, int? bWho, DateTime? bWhoDate, int? lastWho, DateTime? lastWhoDate, int? lastCd4, DateTime? lastCd4Date, int? m12Cd4, DateTime? m12Cd4Date, int? m6Cd4, DateTime? m6Cd4Date, string emr, string project, Guid patientId)
        {
            eCD4 = eCd4;
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
    }
}
