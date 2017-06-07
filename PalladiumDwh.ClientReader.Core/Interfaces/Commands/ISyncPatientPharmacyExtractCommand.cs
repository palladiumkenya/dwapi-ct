using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Commands
{
    public interface ISyncPatientPharmacyExtractCommand : ISyncCommand<TempPatientPharmacyExtract, ClientPatientPharmacyExtract>
    {

    }
}