using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class LoadPatientPharmacyExtractDbCommand : LoadEmrExtractCommand<TempPatientPharmacyExtract>, ILoadPatientPharmacyExtractCommand
  {
      public LoadPatientPharmacyExtractDbCommand(IEMRRepository emrRepository, int batchSize = 100) : base(emrRepository, batchSize)
      {
      }
  }
}
