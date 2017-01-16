using System;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Model.DTO
{
    public class PatientPharmacyExtractDTO
    {
        public int? VisitID { get; set; }
        public string Drug { get; set; }
        public DateTime? DispenseDate { get; set; }
        public decimal? Duration { get; set; }
        public string ExpectedReturn { get; set; }
        public string TreatmentType { get; set; }
        public string PeriodTaken { get; set; }
        public string ProphylaxisType { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public int? Uploaded { get; set; }
        public Guid PatientId { get; set; }

        public PatientPharmacyExtract GeneratePatientPharmacyExtract()
        {
            return new PatientPharmacyExtract(VisitID, Drug, DispenseDate, Duration, ExpectedReturn, TreatmentType,
                PeriodTaken, ProphylaxisType, Emr, Project, Uploaded, PatientId);
        }
    }
}
