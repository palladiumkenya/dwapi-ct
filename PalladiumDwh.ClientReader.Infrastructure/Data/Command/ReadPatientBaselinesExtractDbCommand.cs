using System.Data;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class ReadPatientBaselinesExtractDbCommand : ExtractDbCommand<PatientBaselinesExtractRow>, IReadPatientBaselinesExtractCommand
  {
      public ReadPatientBaselinesExtractDbCommand(IDbConnection connection, string commandText) : base(connection, commandText)
      {
      }
  }
}
