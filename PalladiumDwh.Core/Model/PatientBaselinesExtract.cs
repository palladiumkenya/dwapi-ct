using System;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Model
{
    public class PatientBaselinesExtract : Entity
    {
        public int PatientId { get; set; }
        public int SiteCode { get; set; }
        public string PatientCccNumber { get; set; }
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
        public int? Uploaded { get; set; }
    
        public virtual PatientExtract PatientExtract { get; set; }
    }
}
