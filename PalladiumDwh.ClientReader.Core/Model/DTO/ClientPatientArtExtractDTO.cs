using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.ClientReader.Core.Interfaces.DTOs;

namespace PalladiumDwh.ClientReader.Core.Model.DTO
{
    public class ClientPatientArtExtractDTO : IClientPatientArtExtractDTO
    {
        public int PatientPID { get; set; }
        public string PatientCccNumber { get; set; }
        public int FacilityId { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public DateTime? DOB { get; set; }
        public decimal? AgeEnrollment { get; set; }
        public decimal? AgeARTStart { get; set; }
        public decimal? AgeLastVisit { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string PatientSource { get; set; }
        public string Gender { get; set; }
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
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }

        public ClientPatientArtExtractDTO()
        {
        }

        public ClientPatientArtExtractDTO(int patientPid, string patientCccNumber, int facilityId, string emr, string project, DateTime? dob, decimal? ageEnrollment, decimal? ageArtStart, decimal? ageLastVisit, DateTime? registrationDate, string patientSource, string gender, DateTime? startArtDate, DateTime? previousArtStartDate, string previousArtRegimen, DateTime? startArtAtThisFacility, string startRegimen, string startRegimenLine, DateTime? lastArtDate, string lastRegimen, string lastRegimenLine, decimal? duration, DateTime? expectedReturn, string provider, DateTime? lastVisit, string exitReason, DateTime? exitDate, DateTime? date_Created,DateTime? date_Last_Modified)
        {
            PatientPID = patientPid;
            PatientCccNumber = patientCccNumber;
            FacilityId = facilityId;
            Emr = emr;
            Project = project;
            DOB = dob;
            AgeEnrollment = ageEnrollment;
            AgeARTStart = ageArtStart;
            AgeLastVisit = ageLastVisit;
            RegistrationDate = registrationDate;
            PatientSource = patientSource;
            Gender = gender;
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
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
        }

        public ClientPatientArtExtractDTO(ClientPatientArtExtract extract)
        {
            PatientPID = extract.PatientPK; //TODO PatientPID = extract.PatientPK;
            PatientCccNumber = extract.PatientID; //TODO PatientCccNumber = extract.PatientID;
            FacilityId = extract.SiteCode; //TODO FacilityId = extract.SiteCode
            Emr = extract.Emr;
            Project = extract.Project;
            DOB = extract.DOB;
            AgeEnrollment = extract.AgeEnrollment;
            AgeARTStart = extract.AgeARTStart;
            AgeLastVisit = extract.AgeLastVisit;
            RegistrationDate = extract.RegistrationDate;
            PatientSource = extract.PatientSource;
            Gender = extract.Gender;
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
            Date_Created = extract.Date_Created;
            Date_Last_Modified = extract.Date_Last_Modified;

        }

        public IEnumerable<ClientPatientArtExtractDTO> GeneratePatientArtExtractDtOs(IEnumerable<ClientPatientArtExtract> extracts)
        {
            var artExtracts = new List<ClientPatientArtExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                artExtracts.Add(new ClientPatientArtExtractDTO(e));
            }
            return artExtracts;
        }

        public string PreviousARTUse { get; set; }
        public string PreviousARTPurpose { get; set; }
        public DateTime? DateLastUsed { get; set; }
    }
}
