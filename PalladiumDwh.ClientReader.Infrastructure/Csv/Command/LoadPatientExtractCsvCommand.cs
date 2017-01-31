using System.Data;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.ClientReader.Core.Model.Source.Map;

namespace PalladiumDwh.ClientReader.Infrastructure.Csv.Command
{
  public  class LoadPatientExtractCsvCommand: LoadExtractCsvCommand<TempPatientExtract>, ILoadPatientExtractCommand
  {
      public LoadPatientExtractCsvCommand(IDbConnection clientConnection, string commandText, int batchSize = 100) : base(clientConnection, commandText, batchSize)
      {
      }
  }
}
