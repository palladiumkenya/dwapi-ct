using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;
using PalladiumDwh.Shared.Interfaces.Extracts;

namespace PalladiumDwh.Shared.Model.Extract
{

    public class PatientExtract:Entity, IPatientExtract
    {
        public int PatientPID { get; set; }
        public string PatientCccNumber { get; set; }
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
        public Guid FacilityId { get; set; }
        public DateTime? Created { get; set; }
        public string Pkv { get; set; }
        public string Occupation { get; set; }

        public virtual ICollection<PatientArtExtract> PatientArtExtracts { get; set; }=new List<PatientArtExtract>();
        public virtual ICollection<PatientBaselinesExtract> PatientBaselinesExtracts { get; set; }=new List<PatientBaselinesExtract>();
        public virtual ICollection<PatientLaboratoryExtract> PatientLaboratoryExtracts { get; set; }=new List<PatientLaboratoryExtract>();
        public virtual ICollection<PatientPharmacyExtract> PatientPharmacyExtracts { get; set; }=new List<PatientPharmacyExtract>();
        public virtual ICollection<PatientStatusExtract> PatientStatusExtracts { get; set; }=new List<PatientStatusExtract>();
        public virtual ICollection<PatientVisitExtract> PatientVisitExtracts { get; set; }=new List<PatientVisitExtract>();
        public virtual ICollection<PatientAdverseEventExtract> PatientAdverseEventExtracts { get; set; } = new List<PatientAdverseEventExtract>();


        public virtual ICollection<AllergiesChronicIllnessExtract> AllergiesChronicIllnessExtracts { get; set; } =
            new List<AllergiesChronicIllnessExtract>();

        public virtual ICollection<ContactListingExtract> ContactListingExtracts { get; set; } =
            new List<ContactListingExtract>();

        public virtual ICollection<DepressionScreeningExtract> DepressionScreeningExtracts { get; set; } =
            new List<DepressionScreeningExtract>();

        public virtual ICollection<EnhancedAdherenceCounsellingExtract> EnhancedAdherenceCounsellingExtracts
        {
            get;
            set;
        } = new List<EnhancedAdherenceCounsellingExtract>();

        public virtual ICollection<DrugAlcoholScreeningExtract> DrugAlcoholScreeningExtracts { get; set; } =
            new List<DrugAlcoholScreeningExtract>();

        public virtual ICollection<GbvScreeningExtract> GbvScreeningExtracts { get; set; } = new List<GbvScreeningExtract>();
        public virtual ICollection<IptExtract> IptExtracts { get; set; } = new List<IptExtract>();
        public virtual ICollection<OtzExtract> OtzExtracts { get; set; } = new List<OtzExtract>();
        public virtual ICollection<OvcExtract> OvcExtracts { get; set; } = new List<OvcExtract>();


        public PatientExtract()
        {
            Created = DateTime.Now;
        }


        public PatientExtract(int patientPid, string patientCccNumber, string gender, DateTime? dob, DateTime? registrationDate, DateTime? registrationAtCcc, DateTime? registrationAtpmtct, DateTime? registrationAtTbClinic, string patientSource, string region, string district, string village, string contactRelation, DateTime? lastVisit, string maritalStatus, string educationLevel, DateTime? dateConfirmedHivPositive, string previousArtExposure, DateTime? previousArtStartDate, string statusAtCcc, string statusAtPmtct, string statusAtTbClinic, Guid facilityId,string emr,string project,
            string orphan, string inschool, string patientType, string populationType, string keyPopulationType, string patientResidentCounty, string patientResidentSubCounty, string patientResidentLocation, string patientResidentSubLocation, string patientResidentWard, string patientResidentVillage, DateTime? transferInDate)

        {
            PatientPID = patientPid;
            PatientCccNumber = patientCccNumber;
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
            FacilityId = facilityId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
            Orphan = orphan;
            Inschool = inschool;
            PatientType = patientType;
            PopulationType = populationType;
            KeyPopulationType = keyPopulationType;
            PatientResidentCounty = patientResidentCounty;
            PatientResidentSubCounty = patientResidentSubCounty;
            PatientResidentLocation = patientResidentLocation;
            PatientResidentSubLocation = patientResidentSubLocation;
            PatientResidentWard = patientResidentWard;
            PatientResidentVillage = patientResidentVillage;
            TransferInDate = transferInDate;
        }

        public void AddPatientArtExtracts(IEnumerable<PatientArtExtract> extracts)
        {
            foreach (var e in extracts)
            {
                e.PatientId = Id;
                PatientArtExtracts.Add(e);
            }
        }
        public void AddPatientBaselinesExtracts(IEnumerable<PatientBaselinesExtract> extracts)
        {
            foreach (var e in extracts)
            {
                e.PatientId = Id;
                PatientBaselinesExtracts.Add(e);
            }
        }
        public void AddPatientLaboratoryExtracts(IEnumerable<PatientLaboratoryExtract> extracts)
        {
            foreach (var e in extracts)
            {
                e.PatientId = Id;
                PatientLaboratoryExtracts.Add(e);
            }
        }
        public void AddPatientPharmacyExtracts(IEnumerable<PatientPharmacyExtract> extracts)
        {
            foreach (var e in extracts)
            {
                e.PatientId = Id;
                PatientPharmacyExtracts.Add(e);
            }
        }
        public void AddPatientStatusExtracts(IEnumerable<PatientStatusExtract> extracts)
        {
            foreach (var e in extracts)
            {
                e.PatientId = Id;
                PatientStatusExtracts.Add(e);
            }
        }
        public void AddPatientVisitExtracts(IEnumerable<PatientVisitExtract> extracts)
        {
            foreach (var e in extracts)
            {
                e.PatientId = Id;
                PatientVisitExtracts.Add(e);
            }
        }

        public void AddPatientAdverseEventExtracts(IEnumerable<PatientAdverseEventExtract> extracts)
        {
            foreach (var e in extracts)
            {
                e.PatientId = Id;
                PatientAdverseEventExtracts.Add(e);
            }
        }

        public bool IsInitialized()
        {
            return string.IsNullOrWhiteSpace(Gender);
        }


    }
}
