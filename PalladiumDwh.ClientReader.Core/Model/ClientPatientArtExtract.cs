using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Core.Model
{
    [Table("PatientArtExtract")]
    public class ClientPatientArtExtract : ClientExtract, IClientPatientArtExtract
    {
        [Key]
        public override Guid Id { get; set; }
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

        public ClientPatientArtExtract()
        {
        }

        public ClientPatientArtExtract(int patientPk, string patientId, int siteCode,DateTime? dob, decimal? ageEnrollment, decimal? ageArtStart,
            decimal? ageLastVisit, DateTime? registrationDate, string gender, string patientSource,
            DateTime? startArtDate, DateTime? previousArtStartDate, string previousArtRegimen,
            DateTime? startArtAtThisFacility, string startRegimen, string startRegimenLine, DateTime? lastArtDate,
            string lastRegimen, string lastRegimenLine, decimal? duration, DateTime? expectedReturn, string provider,
            DateTime? lastVisit, string exitReason, DateTime? exitDate, string emr, string project)
        {
            PatientPK = patientPk;
            PatientID = patientId;
            SiteCode = siteCode;
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
        }

        public ClientPatientArtExtract(TempPatientArtExtract extract)
        {
            PatientPK = extract.PatientPK.Value;
            PatientID = extract.PatientID;
            SiteCode = extract.SiteCode.Value;
            DOB = extract.DOB;
            AgeEnrollment = extract.AgeEnrollment;
            AgeARTStart = extract.AgeARTStart;
            AgeLastVisit = extract.AgeLastVisit;
            RegistrationDate = extract.RegistrationDate;
            Gender = extract.Gender;
            PatientSource = extract.PatientSource;
            StartARTDate = extract.StartARTDate;
            PreviousARTStartDate = extract.PreviousARTStartDate;
            PreviousARTRegimen = extract.PreviousARTRegimen;
            StartARTAtThisFacility = extract.StartARTAtThisFacility;
            StartRegimen = extract.StartRegimen;
            StartRegimenLine = extract.StartRegimenLine;
            LastARTDate = extract.LastARTDate;
            LastRegimen = extract.LastRegimen;
            LastRegimenLine = extract.LastRegimenLine;
            Duration = extract.Duration;
            ExpectedReturn = extract.ExpectedReturn;
            Provider = extract.Provider;
            LastVisit = extract.LastVisit;
            ExitReason = extract.ExitReason;
            ExitDate = extract.ExitDate;
            Emr = extract.Emr;
            Project = extract.Project;
        }

        public string PreviousARTUse { get; set; }
        public string PreviousARTPurpose { get; set; }
        public DateTime? DateLastUsed { get; set; }
    }
}
