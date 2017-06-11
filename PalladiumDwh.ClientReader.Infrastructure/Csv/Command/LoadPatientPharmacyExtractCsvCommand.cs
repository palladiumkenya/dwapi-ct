using System.Data;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Csv.Command
{
    public class LoadPatientPharmacyExtractCsvCommand : LoadExtractCsvCommand<TempPatientPharmacyExtract>, ILoadPatientPharmacyExtractCsvCommand
    {
        public LoadPatientPharmacyExtractCsvCommand(IEMRRepository emrRepository, int batchSize = 100) : base(emrRepository, batchSize)
        {
        }
    }
}