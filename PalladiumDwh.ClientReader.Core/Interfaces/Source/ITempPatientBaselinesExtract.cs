using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Source
{
    public interface ITempPatientBaselinesExtract: ITempExtract,IBaseline
    {

        string Emr { get; set; }
        string Project { get; set; }

    }
}