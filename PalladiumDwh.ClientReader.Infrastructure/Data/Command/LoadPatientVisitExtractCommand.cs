using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class LoadPatientVisitExtractCommand : LoadExtractCommand<TempPatientVisitExtract>, ILoadPatientVisitExtractCommand
  {
      public LoadPatientVisitExtractCommand(IEMRRepository emrRepository, int batchSize = 1000) : base(emrRepository, batchSize)
      {
      }
  }
}
