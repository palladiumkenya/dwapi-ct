using System;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Model
{
    public  class PatientLaboratoryExtract:Entity
    {
        public int PatientId { get; set; }
        public int SiteCode { get; set; }
        public string PatientCccNumber { get; set; }
        public int? VisitId { get; set; }
        public DateTime? OrderedByDate { get; set; }
        public DateTime? ReportedByDate { get; set; }
        public string TestName { get; set; }
        public string TestResult { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public int? Uploaded { get; set; }
    
        public virtual PatientExtract PatientExtract { get; set; }
    }
}
