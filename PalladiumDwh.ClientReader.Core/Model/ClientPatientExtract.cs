using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PalladiumDwh.ClientReader.Core.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Model
{
    [Table("PatientExtract")]
    public class ClientPatientExtract: ClientExtract, IClientPatientExtract
    {
        
        [Key, Column(Order = 1)]
        public override int PatientPK { get; set; }
        [Key, Column(Order = 2)]
        public override int SiteCode { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? RegistrationAtCCC { get; set; }
        public DateTime? RegistrationATPMTCT { get; set; }
        public DateTime? RegistrationAtTBClinic { get; set; }
        public string PatientSource { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string Village { get; set; }
        public string ContactRelation { get; set; }
        public DateTime? LastVisit { get; set; }
        public string MaritalStatus { get; set; }
        public string EducationLevel { get; set; }
        public DateTime? DateConfirmedHIVPositive { get; set; }
        public string PreviousARTExposure { get; set; }
        public DateTime? PreviousARTStartDate { get; set; }
        public string StatusAtCCC { get; set; }
        public string StatusAtPMTCT { get; set; }
        public string StatusAtTBClinic { get; set; }
        

        public virtual ICollection<ClientPatientArtExtract> ClientPatientArtExtracts { get; set; }=new List<ClientPatientArtExtract>();
        public virtual ICollection<ClientPatientBaselinesExtract> ClientPatientBaselinesExtracts { get; set; }=new List<ClientPatientBaselinesExtract>();
        public virtual ICollection<ClientPatientLaboratoryExtract> ClientPatientLaboratoryExtracts { get; set; }=new List<ClientPatientLaboratoryExtract>();
        public virtual ICollection<ClientPatientPharmacyExtract> ClientPatientPharmacyExtracts { get; set; }=new List<ClientPatientPharmacyExtract>();
        public virtual ICollection<ClientPatientStatusExtract> ClientPatientStatusExtracts { get; set; }=new List<ClientPatientStatusExtract>();
        public virtual ICollection<ClientPatientVisitExtract> ClientPatientVisitExtracts { get; set; }=new List<ClientPatientVisitExtract>();

        public ClientPatientExtract()
        {
        }

        public ClientPatientExtract(string gender, DateTime? dob, DateTime? registrationDate, DateTime? registrationAtCcc, DateTime? registrationAtpmtct, DateTime? registrationAtTbClinic, string patientSource, string region, string district, string village, string contactRelation, DateTime? lastVisit, string maritalStatus, string educationLevel, DateTime? dateConfirmedHivPositive, string previousArtExposure, DateTime? previousArtStartDate, string statusAtCcc, string statusAtPmtct, string statusAtTbClinic, string emr, string project)
        {
            Gender = gender;
            DOB = dob;
            RegistrationDate = registrationDate;
            RegistrationAtCCC = registrationAtCcc;
            RegistrationATPMTCT = registrationAtpmtct;
            RegistrationAtTBClinic = registrationAtTbClinic;
            PatientSource = patientSource;
            Region = region;
            District = district;
            Village = village;
            ContactRelation = contactRelation;
            LastVisit = lastVisit;
            MaritalStatus = maritalStatus;
            EducationLevel = educationLevel;
            DateConfirmedHIVPositive = dateConfirmedHivPositive;
            PreviousARTExposure = previousArtExposure;
            PreviousARTStartDate = previousArtStartDate;
            StatusAtCCC = statusAtCcc;
            StatusAtPMTCT = statusAtPmtct;
            StatusAtTBClinic = statusAtTbClinic;
            Emr = emr;
            Project = project;
        }
    }
}
