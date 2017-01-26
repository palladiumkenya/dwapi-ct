using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.ExtractRows
{
    public interface IPatientVisitExtractRow : IExtractRow, IVisit
    {
        string FacilityName { get; set; }
    }
}