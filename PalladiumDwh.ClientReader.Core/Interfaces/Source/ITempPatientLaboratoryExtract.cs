using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Source
{
    public interface ITempPatientLaboratoryExtract:ITempExtract,ILaboratory
    {
        string FacilityName { get; set; }
        string SatelliteName { get; set; }
        string Emr { get; set; }
        string Project { get; set; }
    }
}