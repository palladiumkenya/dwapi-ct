using System;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Model
{
    public class PatientStatusExtract : Entity
    {
        public string ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
    }
}
