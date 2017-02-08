using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class SyncPatientBaselinesExtractDbCommand : SyncCommand<TempPatientBaselinesExtract, ClientPatientBaselinesExtract>, ISyncPatientBaselinesExtractCommand
    {
      public SyncPatientBaselinesExtractDbCommand(string connectionString) : base(connectionString)
      {
      }
  }
}
