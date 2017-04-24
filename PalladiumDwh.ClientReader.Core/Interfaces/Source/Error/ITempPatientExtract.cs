using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Source.Error
{
    public interface ITempPatientExtractError: ITempExtractError,IPatient
    {
        string FacilityName { get; set; }
        string SatelliteName { get; set; }
         string Emr { get; set; }
        string Project { get; set; }
    }
}