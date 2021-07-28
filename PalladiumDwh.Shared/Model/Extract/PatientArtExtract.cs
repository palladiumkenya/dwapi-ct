using System;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
{
    public class PatientArtExtract:Entity, IPatientArtExtract
    {
        public DateTime? DOB { get; set; }
        public decimal? AgeEnrollment { get; set; }
        public decimal? AgeARTStart { get; set; }
        public decimal? AgeLastVisit { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string Gender { get; set; }
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
        public string Provider { get; set; }
        public DateTime? LastVisit { get; set; }
        public string ExitReason { get; set; }
        public DateTime? ExitDate { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? Created { get; set; }

        public string PreviousARTUse { get; set; }
        public string PreviousARTPurpose { get; set; }
        public DateTime? DateLastUsed { get; set; }

        public PatientArtExtract()
        {
            Created=DateTime.Now;
        }

        public PatientArtExtract(DateTime? dob, decimal? ageEnrollment, decimal? ageArtStart, decimal? ageLastVisit, DateTime? registrationDate, string gender, string patientSource, DateTime? startArtDate, DateTime? previousArtStartDate, string previousArtRegimen, DateTime? startArtAtThisFacility, string startRegimen, string startRegimenLine, DateTime? lastArtDate, string lastRegimen, string lastRegimenLine, decimal? duration, DateTime? expectedReturn, string provider, DateTime? lastVisit, string exitReason, DateTime? exitDate, Guid patientId, string emr, string project,
        string previousARTUse,	string previousARTPurpose,	DateTime? dateLastUsed
        )
        {
            DOB = dob;
            AgeEnrollment = ageEnrollment;
            AgeARTStart = ageArtStart;
            AgeLastVisit = ageLastVisit;
            RegistrationDate = registrationDate;
            Gender = gender;
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
            Provider = provider;
            LastVisit = lastVisit;
            ExitReason = exitReason;
            ExitDate = exitDate;
            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
            PreviousARTUse = previousARTUse;
            PreviousARTPurpose = previousARTPurpose;
            DateLastUsed = dateLastUsed;

        }
    }
}
