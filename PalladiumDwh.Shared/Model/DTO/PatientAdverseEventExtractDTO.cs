using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class PatientAdverseEventExtractDTO : IPatientAdverseEventExtractDTO
    {
        public string AdverseEvent { get; set; }
        public DateTime? AdverseEventStartDate { get; set; }
        public DateTime? AdverseEventEndDate { get; set; }
        public string Severity { get; set; }
        public string AdverseEventClinicalOutcome { get; set; }
        public string AdverseEventActionTaken { get; set; }
        public bool? AdverseEventIsPregnant { get; set; }
        public DateTime? VisitDate { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }

        public PatientAdverseEventExtractDTO()
        {
        }

        public PatientAdverseEventExtractDTO(string adverseEvent, DateTime? adverseEventStartDate, DateTime? adverseEventEndDate, string severity, string adverseEventClinicalOutcome, string adverseEventActionTaken, 
            bool? adverseEventIsPregnant, DateTime? visitDate,string emr, string project, Guid patientId)
        {
            AdverseEvent = adverseEvent;
            AdverseEventStartDate = adverseEventStartDate;
            AdverseEventEndDate = adverseEventEndDate;
            Severity = severity;
            AdverseEventClinicalOutcome = adverseEventClinicalOutcome;
            AdverseEventActionTaken = adverseEventActionTaken;
            AdverseEventIsPregnant = adverseEventIsPregnant;
            VisitDate = visitDate;
            Emr = emr;
            Project = project;
            PatientId = patientId;
        }

        public PatientAdverseEventExtractDTO(PatientAdverseEventExtract patientStatusExtract)
        {

            AdverseEvent = patientStatusExtract.AdverseEvent;
            AdverseEventStartDate = patientStatusExtract.AdverseEventStartDate;
            AdverseEventEndDate = patientStatusExtract.AdverseEventEndDate;
            Severity = patientStatusExtract.Severity;
            AdverseEventClinicalOutcome = patientStatusExtract.AdverseEventClinicalOutcome;
            AdverseEventActionTaken = patientStatusExtract.AdverseEventActionTaken;
            AdverseEventIsPregnant = patientStatusExtract.AdverseEventIsPregnant;
            Emr = patientStatusExtract.Emr;
            Project = patientStatusExtract.Project;
            PatientId = patientStatusExtract.PatientId;
        }



        public IEnumerable<PatientAdverseEventExtractDTO> GeneratePatientAdverseEventExtractDtOs(IEnumerable<PatientAdverseEventExtract> extracts)
        {
            var statusExtractDtos = new List<PatientAdverseEventExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new PatientAdverseEventExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public PatientAdverseEventExtract GeneratePatientAdverseEventExtract(Guid patientId)
        {
            PatientId = patientId;
            return new PatientAdverseEventExtract(
                AdverseEvent,
                AdverseEventStartDate,
                AdverseEventEndDate,
                Severity,
                AdverseEventClinicalOutcome,
                AdverseEventActionTaken,
                AdverseEventIsPregnant,
                VisitDate,
                PatientId, Emr, Project);
        }
    }
}