using System;
using PalladiumDwh.ClientReader.Core.Interfaces.Source;

namespace PalladiumDwh.ClientReader.Core.Model.Source
{

    public class TempPatientPharmacyExtract : TempExtract, ITempPatientPharmacyExtract
    {



        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }

        public int? VisitID { get; set; }
        public string Drug { get; set; }
        public string Provider { get; set; }
        public DateTime? DispenseDate { get; set; }
        public decimal? Duration { get; set; }
        public DateTime? ExpectedReturn { get; set; }
        public string TreatmentType { get; set; }
        public string RegimenLine { get; set; }
        public string PeriodTaken { get; set; }
        public string ProphylaxisType { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public string RegimenChangedSwitched { get; set; }
        public string RegimenChangeSwitchReason { get; set; }
        public string StopRegimenReason { get; set; }
        public DateTime? StopRegimenDate { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
    }
}
