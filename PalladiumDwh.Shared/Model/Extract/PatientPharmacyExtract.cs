using System;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
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
        public DateTime? Created { get; set; }

        public string RegimenChangedSwitched { get; set; }
        public string RegimenChangeSwitchReason { get; set; }
        public string StopRegimenReason { get; set; }
        public DateTime? StopRegimenDate { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }


        public PatientPharmacyExtract()
        {
            Created = DateTime.Now;
        }

        public PatientPharmacyExtract(int? visitId, string drug, string provider, DateTime? dispenseDate, decimal? duration, DateTime? expectedReturn, string treatmentType, string regimenLine, string periodTaken, string prophylaxisType, Guid patientId, string emr, string project,
            string regimenChangedSwitched, string regimenChangeSwitchReason, string stopRegimenReason, DateTime? stopRegimenDate, DateTime? date_Created,DateTime? date_Last_Modified,string recordUUID)
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
            Created = DateTime.Now;
            RecordUUID = recordUUID;


            RegimenChangedSwitched = regimenChangedSwitched;
            RegimenChangeSwitchReason = regimenChangeSwitchReason;
            StopRegimenReason = stopRegimenReason;
            StopRegimenDate = stopRegimenDate;
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
            this.StandardizeExtract();
        }


    }
}
