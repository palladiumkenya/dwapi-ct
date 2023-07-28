using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Core.Application.Extracts.Source.Dto
{
    public class AdverseEventSourceDto:SourceDto, IAdverseEvent
    {
        public string AdverseEvent { get; set; }
        public DateTime? AdverseEventStartDate { get; set; }
        public DateTime? AdverseEventEndDate { get; set; }
        public string Severity { get; set; }
        public string AdverseEventClinicalOutcome { get; set; }
        public string AdverseEventActionTaken { get; set; }
        public bool? AdverseEventIsPregnant { get; set; }
        public DateTime? VisitDate { get; set; }
        public string AdverseEventRegimen { get; set; }
        public string AdverseEventCause { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string PatientUUID { get; set; }

    }
}
