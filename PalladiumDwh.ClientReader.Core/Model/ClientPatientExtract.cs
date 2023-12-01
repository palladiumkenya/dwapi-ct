using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.ClientReader.Core.Model
{
    [Table("PatientExtract")]
    public class ClientPatientExtract: ClientExtract, IClientPatientExtract
    {

        [Key, Column(Order = 1)]
        public override int PatientPK { get; set; }
        [Key, Column(Order = 2)]
        public override int SiteCode { get; set; }
        public string FacilityName { get; set; }
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
        public string Orphan { get; set; }
        public string Inschool { get; set; }
        public string PatientType { get; set; }
        public string PopulationType { get; set; }
        public string KeyPopulationType { get; set; }
        public string PatientResidentCounty { get; set; }
        public string PatientResidentSubCounty { get; set; }
        public string PatientResidentLocation { get; set; }
        public string PatientResidentSubLocation { get; set; }
        public string PatientResidentWard { get; set; }
        public string PatientResidentVillage { get; set; }
        public DateTime? TransferInDate { get; set; }
        public string NUPI { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }


        [DoNotRead]
        public virtual ICollection<ClientPatientArtExtract> ClientPatientArtExtracts { get; set; }=new List<ClientPatientArtExtract>();
        [DoNotRead]
        public virtual ICollection<ClientPatientBaselinesExtract> ClientPatientBaselinesExtracts { get; set; }=new List<ClientPatientBaselinesExtract>();
        [DoNotRead]
        public virtual ICollection<ClientPatientLaboratoryExtract> ClientPatientLaboratoryExtracts { get; set; }=new List<ClientPatientLaboratoryExtract>();
        [DoNotRead]
        public virtual ICollection<ClientPatientPharmacyExtract> ClientPatientPharmacyExtracts { get; set; }=new List<ClientPatientPharmacyExtract>();
        [DoNotRead]
        public virtual ICollection<ClientPatientStatusExtract> ClientPatientStatusExtracts { get; set; }=new List<ClientPatientStatusExtract>();
        [DoNotRead]
        public virtual ICollection<ClientPatientVisitExtract> ClientPatientVisitExtracts { get; set; }=new List<ClientPatientVisitExtract>();
        [DoNotRead]
        public override bool? Processed { get; set; }

        public ClientPatientExtract()
        {
        }

        public ClientPatientExtract(int patientPk, string patientId, int siteCode, string facilityName,string gender, DateTime? dob, DateTime? registrationDate, DateTime? registrationAtCcc, DateTime? registrationAtpmtct, DateTime? registrationAtTbClinic, string patientSource, string region, string district, string village, string contactRelation, DateTime? lastVisit, string maritalStatus, string educationLevel, DateTime? dateConfirmedHivPositive, string previousArtExposure, DateTime? previousArtStartDate, string statusAtCcc, string statusAtPmtct, string statusAtTbClinic, string emr, string project, string nupi, DateTime? date_Created,DateTime? date_Last_Modified,string recordUUID)
        {
            PatientPK = patientPk;
            PatientID = patientId;
            SiteCode = siteCode;
            FacilityName = facilityName;
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
            NUPI = nupi;
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
            RecordUUID = recordUUID;
        }

        public ClientPatientExtract(TempPatientExtract extract)
        {
            PatientPK = extract.PatientPK.Value;
            PatientID = extract.PatientID;
            SiteCode = extract.SiteCode.Value;
            FacilityName = extract.FacilityName;
            Gender = extract.Gender;
            DOB = extract.DOB;
            RegistrationDate = extract.RegistrationDate;
            RegistrationAtCCC = extract.RegistrationAtCCC;
            RegistrationATPMTCT = extract.RegistrationATPMTCT;
            RegistrationAtTBClinic = extract.RegistrationAtTBClinic;
            PatientSource = extract.PatientSource;
            Region = extract.Region;
            District = extract.District;
            Village = extract.Village;
            ContactRelation = extract.ContactRelation;
            LastVisit = extract.LastVisit;
            MaritalStatus = extract.MaritalStatus;
            EducationLevel = extract.EducationLevel;
            DateConfirmedHIVPositive = extract.DateConfirmedHIVPositive;
            PreviousARTExposure = extract.PreviousARTExposure;
            PreviousARTStartDate = extract.PreviousARTStartDate;
            StatusAtCCC = extract.StatusAtCCC;
            StatusAtPMTCT = extract.StatusAtPMTCT;
            StatusAtTBClinic = extract.StatusAtTBClinic;
            Emr = extract.Emr;
            Project = extract.Project;
            NUPI = extract.NUPI;
            Date_Created = extract.Date_Created;
            Date_Last_Modified = extract.Date_Last_Modified;
            RecordUUID = extract.RecordUUID;
        }



        public override string GetAddAction(string source, bool lookup = true)
        {
            string sql = $@"
            (SELECT      
	            p.*
            FROM            
	            {source} AS p INNER JOIN
	            (SELECT Id, ROW_NUMBER() OVER (PARTITION BY PatientPK,Sitecode ORDER BY PatientPK,Sitecode) AS RW
	            FROM {source}) AS i ON p.Id = i.Id
            WHERE        
	            (i.RW = 1))xx
            WHERE xx.CheckError = 0
            ";
            return base.GetAddAction(sql, false);
        }

        public bool HasArt()
        {
            return ClientPatientArtExtracts.Count > 0;
        }
        public bool HasBaselines()
        {
            return ClientPatientBaselinesExtracts.Count > 0;
        }
        public bool HasLabs()
        {
            return ClientPatientLaboratoryExtracts.Count > 0;
        }
        public bool HasPharmacy()
        {
            return ClientPatientPharmacyExtracts.Count > 0;
        }
        public bool HasVisits()
        {
            return ClientPatientVisitExtracts.Count > 0;
        }
        public bool HasStatus()
        {
            return ClientPatientStatusExtracts.Count > 0;
        }

        public bool IsComplete()
        {
            return HasArt() && HasBaselines() && HasLabs() && HasPharmacy() && HasVisits() && HasStatus();
        }


        public void AddPatientArtExtracts(IEnumerable<ClientPatientArtExtract> extracts)
        {
            foreach (var e in extracts)
            {
                e.PatientPK = PatientPK;
                e.SiteCode = SiteCode;
                ClientPatientArtExtracts.Add(e);
            }
        }
        public void AddPatientBaselinesExtracts(IEnumerable<ClientPatientBaselinesExtract> extracts)
        {
            foreach (var e in extracts)
            {
                e.PatientPK = PatientPK;
                e.SiteCode = SiteCode;
                ClientPatientBaselinesExtracts.Add(e);
            }
        }
        public void AddPatientLaboratoryExtracts(IEnumerable<ClientPatientLaboratoryExtract> extracts)
        {
            foreach (var e in extracts)
            {
                e.PatientPK = PatientPK;
                e.SiteCode = SiteCode;
                ClientPatientLaboratoryExtracts.Add(e);
            }
        }
        public void AddPatientPharmacyExtracts(IEnumerable<ClientPatientPharmacyExtract> extracts)
        {
            foreach (var e in extracts)
            {
                e.PatientPK = PatientPK;
                e.SiteCode = SiteCode;
                ClientPatientPharmacyExtracts.Add(e);
            }
        }
        public void AddPatientStatusExtracts(IEnumerable<ClientPatientStatusExtract> extracts)
        {
            foreach (var e in extracts)
            {
                e.PatientPK = PatientPK;
                e.SiteCode = SiteCode;
                ClientPatientStatusExtracts.Add(e);
            }
        }
        public void AddPatientVisitExtracts(IEnumerable<ClientPatientVisitExtract> extracts)
        {
            foreach (var e in extracts)
            {
                e.PatientPK = PatientPK;
                e.SiteCode = SiteCode;
                ClientPatientVisitExtracts.Add(e);
            }
        }

        public override string ToString()
        {
            return $"{PatientID}";
        }

        public string Pkv { get; set; }
        public string Occupation { get; set; }
    }
}
