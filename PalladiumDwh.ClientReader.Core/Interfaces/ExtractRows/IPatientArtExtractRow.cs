using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.ExtractRows
{
    public interface IPatientArtExtractRow: IExtractRow,IArt
    {
         string FacilityName { get; set; }
    }
}