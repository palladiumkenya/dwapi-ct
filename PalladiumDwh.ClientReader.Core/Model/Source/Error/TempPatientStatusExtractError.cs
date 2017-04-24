using System;
using PalladiumDwh.ClientReader.Core.Interfaces.Source.Error;

namespace PalladiumDwh.ClientReader.Core.Model.Source.Error
{
    
    public class TempPatientStatusExtractError : TempExtractError, ITempPatientStatusExtractError
    {
        

        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }

        public string FacilityName { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public string ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }
    }
}
