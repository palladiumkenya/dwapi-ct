using System;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Extracts
{
    public interface IPatientLaboratoryExtractRow:IExtractRow,ILaboratory
    {
        string FacilityName { get; set; }
        string SatelliteName { get; set; }
    }
}