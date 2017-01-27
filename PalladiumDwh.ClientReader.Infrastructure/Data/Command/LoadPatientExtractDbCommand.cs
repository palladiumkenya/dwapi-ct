using System.Data;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class LoadPatientExtractDbCommand: LoadExtractDbCommand<TempPatientExtract>, ILoadPatientExtractCommand
  {
      public LoadPatientExtractDbCommand(IDbConnection sourceConnection, string commandText) : base(sourceConnection, commandText)
      {
      }
  }
}
