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

        public PatientPharmacyExtractDTO(PatientPharmacyExtract patientPharmacyExtract)
        {
            VisitID = patientPharmacyExtract.VisitID;
            Drug = patientPharmacyExtract.Drug;
            DispenseDate = patientPharmacyExtract.DispenseDate;
            Duration = patientPharmacyExtract.Duration;
            ExpectedReturn = patientPharmacyExtract.ExpectedReturn;
            TreatmentType = patientPharmacyExtract.TreatmentType;
            PeriodTaken = patientPharmacyExtract.PeriodTaken;
            ProphylaxisType = patientPharmacyExtract.ProphylaxisType;
            Emr = patientPharmacyExtract.Emr;
            Project = patientPharmacyExtract.Project;
            Uploaded = patientPharmacyExtract.Uploaded;
            PatientId = patientPharmacyExtract.PatientId;
        }

        public PatientPharmacyExtract GeneratePatientPharmacyExtract()
        {
            return new PatientPharmacyExtract(VisitID, Drug, DispenseDate, Duration, ExpectedReturn, TreatmentType,
                PeriodTaken, ProphylaxisType, Emr, Project, Uploaded, PatientId);
        }
    }
}
