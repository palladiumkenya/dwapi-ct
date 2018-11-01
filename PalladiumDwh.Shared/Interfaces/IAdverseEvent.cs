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
    }
}