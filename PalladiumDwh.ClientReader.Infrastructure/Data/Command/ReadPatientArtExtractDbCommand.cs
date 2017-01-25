using System.Data;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class ReadPatientArtExtractDbCommand: ExtractDbCommand<PatientArtExtractRow>, IReadPatientArtExtractCommand
  {
      public ReadPatientArtExtractDbCommand(IDbConnection connection, string commandText) : base(connection, commandText)
      {
      }
  }
}
