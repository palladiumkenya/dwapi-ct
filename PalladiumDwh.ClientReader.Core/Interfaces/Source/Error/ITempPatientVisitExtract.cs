using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Source.Error
{
    public interface ITempPatientVisitExtractError : ITempExtractError, IVisit
    {
        string FacilityName { get; set; }
        string Emr { get; set; }
        string Project { get; set; }
    }
}