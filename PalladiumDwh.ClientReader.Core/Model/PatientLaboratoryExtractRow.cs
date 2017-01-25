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
    public class PatientLaboratoryExtractRow : ExtractRow, IPatientLaboratoryExtractRow
    {
        public int PatientPK { get; set; }
        public string PatientID { get; set; }
        public int FacilityId { get; set; }
        public int SiteCode { get; set; }
        public string FacilityName { get; set; }
        public string SatelliteName { get; set; }
        public int VisitId { get; set; }
        public DateTime OrderedByDate { get; set; }
        public DateTime ReportedByDate { get; set; }
        public string TestName { get; set; }
        public int EnrollmentTest { get; set; }
        public string TestResult { get; set; }
        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }
    }
}
