using System.Data;
using System.Data.Entity;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class SyncPatientStatusExtractDbCommand : SyncCommand<TempPatientStatusExtract, ClientPatientStatusExtract>, ISyncPatientStatusExtractCommand
    {
      public SyncPatientStatusExtractDbCommand(string connectionString) : base(connectionString)
      {
      }
  }
}
