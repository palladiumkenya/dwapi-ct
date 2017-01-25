using System;
using System.Data;
using System.Linq;
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
    public class PatientArtExtractRow : ExtractRow, IPatientArtExtractRow
    {
        public int PatientPK { get; set; }
        public string PatientID { get; set; }
        public int FacilityId { get; set; }
        public int SiteCode { get; set; }
        public string FacilityName { get; set; }
        public DateTime DOB { get; set; }
        public decimal AgeEnrollment { get; set; }
        public decimal AgeARTStart { get; set; }
        public decimal AgeLastVisit { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Gender { get; set; }
        public DateTime StartARTDate { get; set; }
        public DateTime PreviousARTStartDate { get; set; }
        public string PreviousARTRegimen { get; set; }
        public DateTime StartARTAtThisFacility { get; set; }
        public string StartRegimen { get; set; }
        public string StartRegimenLine { get; set; }
        public DateTime LastARTDate { get; set; }
        public string LastRegimen { get; set; }
        public string LastRegimenLine { get; set; }
        public decimal Duration { get; set; }
        public DateTime ExpectedReturn { get; set; }
        public string Provider { get; set; }
        public DateTime LastVisit { get; set; }
        public string ExitReason { get; set; }
        public DateTime ExitDate { get; set; }
        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }
    }
}
