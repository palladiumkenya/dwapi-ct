using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared;

namespace PalladiumDwh.Core.Model.DTO
{    
    public class PatientArtExtractDTO
    {
        public decimal? AgeEnrollment { get; set; }
        public decimal? AgeARTStart { get; set; }
        public decimal? AgeLastVisit { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string PatientSource { get; set; }
        public DateTime? StartARTDate { get; set; }
        public DateTime? PreviousARTStartDate { get; set; }
        public string PreviousARTRegimen { get; set; }
        public DateTime? StartARTAtThisFacility { get; set; }
        public string StartRegimen { get; set; }
        public string StartRegimenLine { get; set; }
        public DateTime? LastARTDate { get; set; }
        public string LastRegimen { get; set; }
        public string LastRegimenLine { get; set; }
        public decimal? Duration { get; set; }
        public DateTime? ExpectedReturn { get; set; }
        public DateTime? LastVisit { get; set; }
        public string ExitReason { get; set; }
        public DateTime? ExitDate { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }

        public PatientArtExtractDTO()
        {
        }

        public PatientArtExtractDTO(decimal? ageEnrollment, decimal? ageArtStart, decimal? ageLastVisit, DateTime? registrationDate, string patientSource, DateTime? startArtDate, DateTime? previousArtStartDate, string previousArtRegimen, DateTime? startArtAtThisFacility, string startRegimen, string startRegimenLine, DateTime? lastArtDate, string lastRegimen, string lastRegimenLine, decimal? duration, DateTime? expectedReturn, DateTime? lastVisit, string exitReason, DateTime? exitDate, string emr, string project, Guid patientId)
        {
            AgeEnrollment = ageEnrollment;
            AgeARTStart = ageArtStart;
            AgeLastVisit = ageLastVisit;
            RegistrationDate = registrationDate;
            PatientSource = patientSource;
            StartARTDate = startArtDate;
            PreviousARTStartDate = previousArtStartDate;
            PreviousARTRegimen = previousArtRegimen;
            StartARTAtThisFacility = startArtAtThisFacility;
            StartRegimen = startRegimen;
            StartRegimenLine = startRegimenLine;
            LastARTDate = lastArtDate;
            LastRegimen = lastRegimen;
            LastRegimenLine = lastRegimenLine;
            Duration = duration;
            ExpectedReturn = expectedReturn;
            LastVisit = lastVisit;
            ExitReason = exitReason;
            ExitDate = exitDate;
            Emr = emr;
            Project = project;
            PatientId = patientId;
        }

        public PatientArtExtractDTO(PatientArtExtract patientArtExtract)
        {
            AgeEnrollment = patientArtExtract.AgeEnrollment;
            AgeARTStart = patientArtExtract.AgeARTStart;
            AgeLastVisit = patientArtExtract.AgeLastVisit;
            RegistrationDate = patientArtExtract.RegistrationDate;
            PatientSource = patientArtExtract.PatientSource;
            StartARTDate = patientArtExtract.StartARTDate;
            PreviousARTStartDate = patientArtExtract.PreviousARTStartDate;
            PreviousARTRegimen = patientArtExtract.PreviousARTRegimen;
            StartARTAtThisFacility = patientArtExtract.StartARTAtThisFacility;
            StartRegimen = patientArtExtract.StartRegimen;
            StartRegimenLine = patientArtExtract.StartRegimenLine;
            LastARTDate = patientArtExtract.LastARTDate;
            LastRegimen = patientArtExtract.LastRegimen;
            LastRegimenLine = patientArtExtract.LastRegimenLine;
            Duration = patientArtExtract.Duration;
            ExpectedReturn = patientArtExtract.ExpectedReturn;
            LastVisit = patientArtExtract.LastVisit;
            ExitReason = patientArtExtract.ExitReason;
            ExitDate = patientArtExtract.ExitDate;
            Emr = patientArtExtract.Emr;
            Project = patientArtExtract.Project;
            PatientId = patientArtExtract.PatientId;
        }

        public IEnumerable<PatientArtExtractDTO> GeneratePatientArtExtractDtOs(IEnumerable<PatientArtExtract> extracts)
        {
            var artExtracts = new List<PatientArtExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                artExtracts.Add(new PatientArtExtractDTO(e));
            }
            return artExtracts;
        }
        public PatientArtExtract GeneratePatientArtExtract(Guid patientId)
        {
            PatientId = patientId;

            return new PatientArtExtract(
                AgeEnrollment, AgeARTStart, AgeLastVisit, RegistrationDate, PatientSource, StartARTDate,PreviousARTStartDate, 
                PreviousARTRegimen, StartARTAtThisFacility,StartRegimen, StartRegimenLine, LastARTDate, LastRegimen, 
                LastRegimenLine, Duration, ExpectedReturn,LastVisit, ExitReason, ExitDate, Emr, Project, PatientId);
        }       
    }
}
