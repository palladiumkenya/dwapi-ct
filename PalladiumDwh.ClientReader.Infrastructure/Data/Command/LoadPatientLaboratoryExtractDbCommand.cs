using System.Data;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class LoadPatientLaboratoryExtractDbCommand : LoadExtractDbCommand<TempPatientLaboratoryExtract>, ILoadPatientLaboratoryExtractCommand
  {
      public LoadPatientLaboratoryExtractDbCommand(IDbConnection sourceConnection, IDbConnection clientConnection, string commandText, int batchSize = 100) : base(sourceConnection, clientConnection, commandText, batchSize)
      {
      }
  }
}
