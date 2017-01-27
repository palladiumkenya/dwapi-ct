using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Source
{
    public interface ITempPatientArtExtract: ITempExtract,IArt
    {
         string FacilityName { get; set; }
    }
}