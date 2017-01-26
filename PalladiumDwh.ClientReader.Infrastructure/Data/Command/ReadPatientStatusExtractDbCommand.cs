using System.Data;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class ReadPatientStatusExtractDbCommand : ExtractDbCommand<PatientStatusExtractRow>, IReadPatientStatusExtractCommand
  {
      public ReadPatientStatusExtractDbCommand(IDbConnection connection, string commandText) : base(connection, commandText)
      {
      }
  }
}
