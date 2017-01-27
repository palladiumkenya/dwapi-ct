using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Source
{
    public interface ITempPatientVisitExtract : ITempExtract, IVisit
    {
        string FacilityName { get; set; }
    }
}