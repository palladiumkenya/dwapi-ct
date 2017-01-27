using System.Data;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class LoadPatientStatusExtractDbCommand : LoadExtractDbCommand<TempPatientStatusExtract>, ILoadPatientStatusExtractCommand
  {
      public LoadPatientStatusExtractDbCommand(IDbConnection sourceConnection, IDbConnection clientConnection, string commandText) : base(sourceConnection, clientConnection, commandText)
      {
      }
  }
}
