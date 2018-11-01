using System;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
{
    public class PatientAdverseEventExtract : Entity, IAdverseEventExtract
    {
        public string AdverseEvent { get; set; }
        public DateTime? AdverseEventStartDate { get; set; }
        public DateTime? AdverseEventEndDate { get; set; }
        public string Severity { get; set; }
        public string AdverseEventClinicalOutcome { get; set; }
        public string AdverseEventActionTaken { get; set; }
        public bool? AdverseEventIsPregnant { get; set; }
        public DateTime? VisitDate { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? Created { get; set; }

        public PatientAdverseEventExtract()
        {
            Created = DateTime.Now;
        }

        public PatientAdverseEventExtract(string adverseEvent, DateTime? adverseEventStartDate, DateTime? adverseEventEndDate, string severity, string adverseEventClinicalOutcome, 
            string adverseEventActionTaken, bool? adverseEventIsPregnant, DateTime? visitDate, Guid patientId, string emr, string project)
        {
            AdverseEvent = adverseEvent;
            AdverseEventStartDate = adverseEventStartDate;
            AdverseEventEndDate = adverseEventEndDate;
            Severity = severity;
            AdverseEventClinicalOutcome = adverseEventClinicalOutcome;
            AdverseEventActionTaken = adverseEventActionTaken;
            AdverseEventIsPregnant = adverseEventIsPregnant;
            VisitDate = visitDate;
            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
        }
    }
}