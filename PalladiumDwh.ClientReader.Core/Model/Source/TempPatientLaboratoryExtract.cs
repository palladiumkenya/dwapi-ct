using System;
using PalladiumDwh.ClientReader.Core.Interfaces.Source;

namespace PalladiumDwh.ClientReader.Core.Model.Source
{

    public class TempPatientLaboratoryExtract : TempExtract, ITempPatientLaboratoryExtract
    {

        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }

        public string FacilityName { get; set; }
        public string SatelliteName { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public int? VisitId { get; set; }
        public DateTime? OrderedByDate { get; set; }
        public DateTime? ReportedByDate { get; set; }
        public string TestName { get; set; }
        public int? EnrollmentTest { get; set; }
        public string TestResult { get; set; }
        public DateTime? DateSampleTaken { get; set; }
        public string SampleType { get; set; }
        public string Reason { get; set; }

        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string RecordUUID { get; set; }

    }
}
