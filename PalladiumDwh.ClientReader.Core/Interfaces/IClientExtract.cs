using System;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IClientExtract
    {
        int PatientPK { get; set; }
        int SiteCode { get; set; }
        string Emr { get; set; }
        string Project { get; set; }
        bool Processed { get; set; }
        Guid UId { get; set; }
    }
}