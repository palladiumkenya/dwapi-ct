using System;
using PalladiumDwh.ClientReader.Core.Interfaces.ExtractRows;

namespace PalladiumDwh.ClientReader.Core.Model
{
    public class PatientStatusExtractRow : ExtractRow, IPatientStatusExtractRow
    {
        

        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }

        public string FacilityName { get; set; }
        public string ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }
    }
}
