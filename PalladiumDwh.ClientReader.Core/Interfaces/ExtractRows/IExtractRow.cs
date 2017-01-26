using System;
using System.Data;

namespace PalladiumDwh.ClientReader.Core.Interfaces.ExtractRows
{
    public interface IExtractRow
    {
        int PatientPK { get; set; }
        string PatientID { get; set; }
        int? FacilityId { get; set; }
        int SiteCode { get; set; }
        DateTime DateExtracted { get; set; }
        void Load(IDataReader reader);
    }
}