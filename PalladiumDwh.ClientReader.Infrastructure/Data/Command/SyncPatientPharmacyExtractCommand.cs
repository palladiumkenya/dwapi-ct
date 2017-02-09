using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class SyncPatientPharmacyExtractCommand : SyncCommand<TempPatientPharmacyExtract, ClientPatientPharmacyExtract>, ISyncPatientPharmacyExtractCommand
    {
        public SyncPatientPharmacyExtractCommand(IEMRRepository emrRepository) : base(emrRepository)
        {
        }
    }
}
