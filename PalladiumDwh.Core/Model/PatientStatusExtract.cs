using System;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Model
{
    public class PatientStatusExtract : Entity
    {
        public int PatientId { get; set; }
        public int SiteCode { get; set; }
        public string PatientCccNumber { get; set; }
        public string FacilityName { get; set; }
        public string ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public int? Uploaded { get; set; }
    
        public virtual PatientExtract PatientExtract { get; set; }
    }
}
