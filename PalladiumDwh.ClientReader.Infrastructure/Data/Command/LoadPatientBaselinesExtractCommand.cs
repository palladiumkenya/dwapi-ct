using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class LoadPatientBaselinesExtractCommand : LoadExtractCommand<TempPatientBaselinesExtract>, ILoadPatientBaselinesExtractCommand
  {
      public LoadPatientBaselinesExtractCommand(IEMRRepository emrRepository, int batchSize = 100) : base(emrRepository, batchSize)
      {
      }
  }
}
