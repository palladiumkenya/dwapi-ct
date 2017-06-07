using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class TempPatientPharmacyExtractRepository : TempExtractRepository<TempPatientPharmacyExtractError>, ITempPatientPharmacyExtractRepository
    {
        public TempPatientPharmacyExtractRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}