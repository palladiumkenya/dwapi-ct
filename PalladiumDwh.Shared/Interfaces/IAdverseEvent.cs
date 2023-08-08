using System;

namespace PalladiumDwh.Shared.Interfaces
{
    public interface IAdverseEvent
    {
        string AdverseEvent { get; set; }
        DateTime ?AdverseEventStartDate { get; set; }
        DateTime? AdverseEventEndDate { get; set; }
        string Severity { get; set; }
        string AdverseEventClinicalOutcome { get; set; }
        string AdverseEventActionTaken { get; set; }
        bool? AdverseEventIsPregnant { get; set; }
        DateTime? VisitDate { get; set; }
        string AdverseEventRegimen { get; set; }
        string AdverseEventCause { get; set; }
        DateTime? Date_Created { get; set; } 
        DateTime? Date_Last_Modified { get; set; } 
        string PatientUUID { get; set; }

    }
}