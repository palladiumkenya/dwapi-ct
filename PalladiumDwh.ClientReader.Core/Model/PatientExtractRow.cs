using System;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;
using FastMember;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared;

namespace PalladiumDwh.ClientReader.Core.Model
{
    public class PatientExtractRow : IPatientExtractRow
    {
     
        public void Load(IDataReader reader)
        {
            PatientPID = reader.Get<int>(nameof(PatientPID));
            PatientCccNumber = reader.Get<string>(nameof(PatientCccNumber));
            Gender = reader.Get<string>(nameof(Gender));
            DOB = reader.Get<DateTime>(nameof(DOB));
            RegistrationDate = reader.Get<DateTime>(nameof(RegistrationDate));
            RegistrationAtCCC = reader.Get<DateTime>(nameof(RegistrationAtCCC));
            RegistrationATPMTCT = reader.Get<DateTime>(nameof(RegistrationATPMTCT));
            RegistrationAtTBClinic = reader.Get<DateTime>(nameof(RegistrationAtTBClinic));
            PatientSource = reader.Get<string>(nameof(PatientSource));
            Region = reader.Get<string>(nameof(Region));
            District = reader.Get<string>(nameof(District));
            Village = reader.Get<string>(nameof(Village));
            ContactRelation = reader.Get<string>(nameof(ContactRelation));
            LastVisit = reader.Get<DateTime>(nameof(LastVisit));
            MaritalStatus = reader.Get<string>(nameof(MaritalStatus));
            EducationLevel = reader.Get<string>(nameof(EducationLevel));
            DateConfirmedHIVPositive = reader.Get<DateTime>(nameof(DateConfirmedHIVPositive));
            PreviousARTExposure = reader.Get<string>(nameof(PreviousARTExposure));
            PreviousARTStartDate = reader.Get<DateTime>(nameof(PreviousARTStartDate));
            Emr = reader.Get<string>(nameof(Emr));
            Project = reader.Get<string>(nameof(Project));
            FacilityId = reader.Get<int>(nameof(FacilityId));
            FacilityCode = reader.Get<int>(nameof(FacilityCode));
            FacilityName = reader.Get<string>(nameof(FacilityName));
            SatelliteName = reader.Get<string>(nameof(SatelliteName));
            StatusAtCCC=reader.Get<string>(nameof(StatusAtCCC));
            StatusAtPMTCT= reader.Get<string>(nameof(StatusAtPMTCT));
            StatusAtTBClinic= reader.Get<string>(nameof(StatusAtTBClinic));
        }


        public string PatientCccNumber { get; set; }
        public int PatientPID { get; set; }
        public int FacilityId { get; set; }
        public int FacilityCode { get; set; }
        public string FacilityName { get; set; }
        public string SatelliteName { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime RegistrationAtCCC { get; set; }
        public DateTime RegistrationATPMTCT { get; set; }
        public DateTime RegistrationAtTBClinic { get; set; }
        public string PatientSource { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string Village { get; set; }
        public string ContactRelation { get; set; }
        public DateTime LastVisit { get; set; }
        public string MaritalStatus { get; set; }
        public string EducationLevel { get; set; }
        public DateTime DateConfirmedHIVPositive { get; set; }
        public string PreviousARTExposure { get; set; }
        public DateTime PreviousARTStartDate { get; set; }
        public string StatusAtCCC { get; set; }
        public string StatusAtPMTCT { get; set; }
        public string StatusAtTBClinic { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public DateTime DateExtracted { get; set; }

        public PatientExtractRow()
        {
            DateExtracted=DateTime.Now;
        }

        public override string ToString()
        {
            return $"{FacilityCode}-{PatientCccNumber}";
        }
    }
}
