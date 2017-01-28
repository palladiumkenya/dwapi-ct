using System.Data;
using System.Data.Entity;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class EFLoadPatientVisitExtractDbCommand : EFLoadExtractDbCommand<TempPatientVisitExtract>, ILoadPatientVisitExtractCommand
  {
      public EFLoadPatientVisitExtractDbCommand(IDbConnection sourceConnection, DbContext context, string commandText, int batchSize = 100) : base(sourceConnection, context, commandText, batchSize)
      {
      }
  }
}
