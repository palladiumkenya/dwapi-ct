using System.Data;
using System.Data.Entity;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class SyncPatientExtractDbCommand : SyncCommand<TempPatientExtract,ClientPatientExtract>, ISyncPatientExtractCommand
  {
      public SyncPatientExtractDbCommand(string connectionString) : base(connectionString)
      {
      }
  }
}
