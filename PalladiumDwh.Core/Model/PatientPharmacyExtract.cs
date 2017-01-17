using System;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Model
{
    public class PatientPharmacyExtract:Entity
    {
        public int? VisitID { get; set; }
        public string Drug { get; set; }
        public DateTime? DispenseDate { get; set; }
        public decimal? Duration { get; set; }
        public string ExpectedReturn { get; set; }
        public string TreatmentType { get; set; }
        public string PeriodTaken { get; set; }
        public string ProphylaxisType { get; set; }
        public Guid PatientId { get; set; }

        public PatientPharmacyExtract()
        {
        }

        public PatientPharmacyExtract(int? visitId, string drug, DateTime? dispenseDate, decimal? duration, string expectedReturn, string treatmentType, string periodTaken, string prophylaxisType, string emr, string project, Guid patientId)
        {
            VisitID = visitId;
            Drug = drug;
            DispenseDate = dispenseDate;
            Duration = duration;
            ExpectedReturn = expectedReturn;
            TreatmentType = treatmentType;
            PeriodTaken = periodTaken;
            ProphylaxisType = prophylaxisType;
            Emr = emr;
            Project = project;
            
            PatientId = patientId;
        }
    }
}
