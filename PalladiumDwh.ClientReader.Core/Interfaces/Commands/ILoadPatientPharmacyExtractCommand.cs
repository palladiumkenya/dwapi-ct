using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Commands
{
    public interface ILoadPatientPharmacyExtractCommand : ILoadExtractCommand<TempPatientPharmacyExtract>
    {
    }
}