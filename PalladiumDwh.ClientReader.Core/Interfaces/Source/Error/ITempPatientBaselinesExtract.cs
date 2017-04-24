using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Source.Error
{
    public interface ITempPatientBaselinesExtractError: ITempExtractError,IBaseline
    {

        string Emr { get; set; }
        string Project { get; set; }

    }
}