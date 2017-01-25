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
    public class PatientStatusExtractRow : ExtractRow, IPatientStatusExtractRow
    {
        public int PatientPK { get; set; }
        public string PatientID { get; set; }
        public int FacilityId { get; set; }
        public int SiteCode { get; set; }
        public string FacilityName { get; set; }
        public string ExitDescription { get; set; }
        public DateTime ExitDate { get; set; }
        public string ExitReason { get; set; }
        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }
    }
}
