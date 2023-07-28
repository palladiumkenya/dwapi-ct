using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Core.Application.Extracts.Source.Dto
{
    public class CervicalCancerScreeningSourceDto:SourceDto, ICervicalCancerScreening
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
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string PatientUUID { get; set; }

    }
}