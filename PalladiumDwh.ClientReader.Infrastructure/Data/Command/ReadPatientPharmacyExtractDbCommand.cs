using System.Data;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
  public  class ReadPatientPharmacyExtractDbCommand : ExtractDbCommand<PatientPharmacyExtractRow>, IReadPatientPharmacyExtractCommand
  {
      public ReadPatientPharmacyExtractDbCommand(IDbConnection connection, string commandText) : base(connection, commandText)
      {
      }
  }
}
