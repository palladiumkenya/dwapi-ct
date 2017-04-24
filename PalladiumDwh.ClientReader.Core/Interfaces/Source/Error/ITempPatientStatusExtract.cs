using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Source.Error
{
    public interface ITempPatientStatusExtractError:ITempExtractError,IStatus
    {
        string FacilityName { get; set; }
        string Emr { get; set; }
        string Project { get; set; }
    }
}