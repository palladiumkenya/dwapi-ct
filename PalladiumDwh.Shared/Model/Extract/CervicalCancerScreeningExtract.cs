using System;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
{
    public class CervicalCancerScreeningExtract : Entity, ICervicalCancerScreeningExtract
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string VisitType { get; set; }
        public string ScreeningMethod { get; set; }
        public string TreatmentToday { get; set; }
        public string ReferredOut { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public string ScreeningType { get; set; }
        public string ScreeningResult { get; set; }
        public string PostTreatmentComplicationCause { get; set; }
        public string OtherPostTreatmentComplication { get; set; }
        public string ReferralReason { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }

        public CervicalCancerScreeningExtract()
        {
            Created = DateTime.Now;
        }

        public CervicalCancerScreeningExtract(string facilityName, int? visitId, DateTime? visitDate, string visitType, string screeningMethod, string treatmentToday, string referredOut, DateTime? nextAppointmentDate, string screeningType, string screeningResult, string postTreatmentComplicationCause,
            string otherPostTreatmentComplication, string referralReason, Guid patientId, string emr, string project, DateTime? date_Created,DateTime? date_Last_Modified, string recordUUID)
        {
            FacilityName = facilityName;
            VisitID = visitId;
            VisitDate = visitDate;

            VisitType = visitType;
            ScreeningMethod = screeningMethod;
            TreatmentToday = treatmentToday;
            ReferredOut = referredOut;
            NextAppointmentDate = nextAppointmentDate;
            ScreeningType = screeningType;
            ScreeningResult = screeningResult;
            PostTreatmentComplicationCause = postTreatmentComplicationCause;
            OtherPostTreatmentComplication = otherPostTreatmentComplication;
            ReferralReason = referralReason;
            RecordUUID = recordUUID;
            
            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
            this.StandardizeExtract();
        }

        public Guid PatientId { get; set; }
    }
}