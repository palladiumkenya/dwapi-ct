using System;

namespace PalladiumDwh.Shared.Model
{
    public class PatientPharmacyExtract:Entity, IPatientPharmacyExtract
    {
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
        public Guid PatientId { get; set; }

        public PatientPharmacyExtract()
        {
        }

        public PatientPharmacyExtract(int? visitId, string drug, string provider, DateTime? dispenseDate, decimal? duration, DateTime? expectedReturn, string treatmentType, string regimenLine, string periodTaken, string prophylaxisType, Guid patientId, string emr, string project)
        {
            VisitID = visitId;
            Drug = drug;
            Provider = provider;
            DispenseDate = dispenseDate;
            Duration = duration;
            ExpectedReturn = expectedReturn;
            TreatmentType = treatmentType;
            RegimenLine = regimenLine;
            PeriodTaken = periodTaken;
            ProphylaxisType = prophylaxisType;
            PatientId = patientId;
            Emr = emr;
            Project = project;
        }
    }
}
