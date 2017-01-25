using System.Data;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class ReadPatientExtractDbCommand: ExtractDbCommand<PatientExtractRow>, IReadPatientExtractCommand
  {
      public ReadPatientExtractDbCommand(IDbConnection connection, string commandText) : base(connection, commandText)
      {
      }
  }
}
