using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.ExtractRows
{
    public interface IPatientExtractRow: IExtractRow,IPatient
    {
        string FacilityName { get; set; }
        string SatelliteName { get; set; }
         string Emr { get; set; }
        string Project { get; set; }
    }
}