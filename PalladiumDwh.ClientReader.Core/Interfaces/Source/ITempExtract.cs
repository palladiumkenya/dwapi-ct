using System;
using System.Data;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Source
{
    public interface ITempExtract
    {
        Guid Id { get; set; }
        int PatientPK { get; set; }
        string PatientID { get; set; }
        int? FacilityId { get; set; }
        int SiteCode { get; set; }
        DateTime DateExtracted { get; set; }
        void Load(IDataReader reader);
        string GetAddAction();
    }
}