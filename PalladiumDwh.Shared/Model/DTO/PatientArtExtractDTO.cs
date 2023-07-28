using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.Shared.Interfaces.DTOs;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Shared.Model.DTO
{
    public class PatientArtExtractDTO : IPatientArtExtractDTO
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
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public string PreviousARTUse { get; set; }
        public string PreviousARTPurpose { get; set; }
        public DateTime? DateLastUsed { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string PatientUUID { get; set; }


        public PatientArtExtractDTO()
        {
        }

        public PatientArtExtractDTO(DateTime? dob, decimal? ageEnrollment, decimal? ageArtStart, decimal? ageLastVisit, DateTime? registrationDate, string gender, string patientSource, DateTime? startArtDate, DateTime? previousArtStartDate, string previousArtRegimen, DateTime? startArtAtThisFacility, string startRegimen, string startRegimenLine, DateTime? lastArtDate, string lastRegimen, string lastRegimenLine, decimal? duration, DateTime? expectedReturn, string provider, DateTime? lastVisit, string exitReason, DateTime? exitDate, string emr, string project, Guid patientId, DateTime? date_Created,DateTime? date_Last_Modified, string patientUUID)
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
            Emr = emr;
            Project = project;
            PatientId = patientId;
            Date_Created=date_Created;
            Date_Last_Modified=date_Last_Modified;
            PatientUUID=patientUUID;

        }

        public PatientArtExtractDTO(PatientArtExtract patientArtExtract)
        {
            DOB = patientArtExtract.DOB;
            AgeEnrollment = patientArtExtract.AgeEnrollment;
            AgeARTStart = patientArtExtract.AgeARTStart;
            AgeLastVisit = patientArtExtract.AgeLastVisit;
            RegistrationDate = patientArtExtract.RegistrationDate;
            Gender = patientArtExtract.Gender;
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
            Date_Created=patientArtExtract.Date_Created;
            Date_Last_Modified=patientArtExtract.Date_Last_Modified;
            PatientUUID=patientArtExtract.PatientUUID;

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

            return new PatientArtExtract(DOB,
                AgeEnrollment, AgeARTStart, AgeLastVisit, RegistrationDate,Gender, PatientSource, StartARTDate,PreviousARTStartDate,
                PreviousARTRegimen, StartARTAtThisFacility,StartRegimen, StartRegimenLine, LastARTDate, LastRegimen,
                LastRegimenLine, Duration, ExpectedReturn,Provider,LastVisit, ExitReason, ExitDate, PatientId,Emr, Project,
                PreviousARTUse,PreviousARTPurpose,DateLastUsed, Date_Created, Date_Last_Modified,PatientUUID
                );
        }
    }
}
