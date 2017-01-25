using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using FastMember;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Extracts;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.DTO;
using PalladiumDwh.Shared;

namespace PalladiumDwh.ClientReader.Core.Model
{
    public class PatientPharmacyExtractRow : ExtractRow, IPatientPharmacyExtractRow
    {
        public int PatientPK { get; set; }
        public string PatientID { get; set; }
        public int FacilityId { get; set; }
        public int SiteCode { get; set; }
        public int VisitID { get; set; }
        public string Drug { get; set; }
        public string Provider { get; set; }
        public DateTime DispenseDate { get; set; }
        public decimal Duration { get; set; }
        public DateTime ExpectedReturn { get; set; }
        public string TreatmentType { get; set; }
        public string RegimenLine { get; set; }
        public string PeriodTaken { get; set; }
        public string ProphylaxisType { get; set; }
        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }
    }
}
