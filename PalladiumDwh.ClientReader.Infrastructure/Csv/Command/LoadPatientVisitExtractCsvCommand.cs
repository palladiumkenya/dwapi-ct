using System.Data;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Csv.Command
{
    public class LoadPatientVisitExtractCsvCommand : LoadExtractCsvCommand<TempPatientVisitExtract>, ILoadPatientVisitExtractCsvCommand
    {
        public LoadPatientVisitExtractCsvCommand(IDbConnection clientConnection, string commandText, int batchSize = 100) : base(clientConnection, commandText, batchSize)
        {
        }
    }
}