using System;
using DocumentFormat.OpenXml.Spreadsheet;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Source
{
    public interface IExtractErrorSummary
    {
        DateTime? DateGenerated { get; set; }
        int? FacilityId { get; set; }
        string Field { get; set; }
        Guid Id { get; set; }
        string PatientID { get; set; }
        int? PatientPK { get; set; }
        Guid RecordId { get; set; }
        int? SiteCode { get; set; }
        string Summary { get; set; }
        string Type { get; set; }

        void AddHeader(Row row);
        void AddRow(Row row);
    }
}