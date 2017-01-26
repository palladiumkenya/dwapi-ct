using System.Data;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class ReadPatientLaboratoryExtractDbCommand : ExtractDbCommand<PatientLaboratoryExtractRow>, IReadPatientLaboratoryExtractCommand
  {
      public ReadPatientLaboratoryExtractDbCommand(IDbConnection connection, string commandText) : base(connection, commandText)
      {
      }
  }
}
