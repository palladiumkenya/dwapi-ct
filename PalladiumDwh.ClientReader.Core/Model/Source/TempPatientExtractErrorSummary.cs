using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PalladiumDwh.ClientReader.Core.Interfaces.Source;

namespace PalladiumDwh.ClientReader.Core.Model.Source
{
    [Table("vTempPatientExtractErrorSummary")]
    public class TempPatientExtractErrorSummary: IExtractErrorSummary
    {
        [Key]
        public Guid Id { get; set; }
        public string Field { get; set; }
        public string Type { get; set; }
        public string Summary { get; set; }
        public DateTime? DateGenerated { get; set; }
        public int? PatientPK { get; set; }
        public string PatientID { get; set; }
        public int? FacilityId { get; set; }
        public int? SiteCode { get; set; }
        public string FacilityName { get; set; }
        public Guid RecordId { get; set; }

        public override string ToString()
        {
            return $"{SiteCode}-{PatientID}";
        }

    }
}
