using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.ExtractRows
{
    public interface IPatientStatusExtractRow:IExtractRow,IStatus
    {
        string FacilityName { get; set; }
    }
}