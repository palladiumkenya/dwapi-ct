//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PalladiumDwh.Wapi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PatientLaboratoryExtract
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int SiteCode { get; set; }
        public string PatientCccNumber { get; set; }
        public Nullable<int> VisitId { get; set; }
        public Nullable<System.DateTime> OrderedByDate { get; set; }
        public Nullable<System.DateTime> ReportedByDate { get; set; }
        public string TestName { get; set; }
        public string TestResult { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Nullable<int> Uploaded { get; set; }
    
        public virtual PatientExtract PatientExtract { get; set; }
    }
}
