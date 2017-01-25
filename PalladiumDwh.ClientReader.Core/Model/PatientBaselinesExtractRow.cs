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
    public class PatientBaselinesExtractRow : ExtractRow, IPatientBaselinesExtractRow
    {
        public int PatientPK { get; set; }
        public string PatientID { get; set; }
        public int FacilityId { get; set; }
        public int SiteCode { get; set; }
        public int bCD4 { get; set; }
        public DateTime bCD4Date { get; set; }
        public int bWAB { get; set; }
        public DateTime bWABDate { get; set; }
        public int bWHO { get; set; }
        public DateTime bWHODate { get; set; }
        public int eWAB { get; set; }
        public DateTime eWABDate { get; set; }
        public int eCD4 { get; set; }
        public DateTime eCD4Date { get; set; }
        public int eWHO { get; set; }
        public DateTime eWHODate { get; set; }
        public int lastWHO { get; set; }
        public DateTime lastWHODate { get; set; }
        public int lastCD4 { get; set; }
        public DateTime lastCD4Date { get; set; }
        public int m12CD4 { get; set; }
        public DateTime m12CD4Date { get; set; }
        public int m6CD4 { get; set; }
        public DateTime m6CD4Date { get; set; }
        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }
    }
}
