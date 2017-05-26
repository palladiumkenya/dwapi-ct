using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class TempPatientPharmacyExtractErrorSummaryRepository : TempExtractErrorSummaryRepository<TempPatientPharmacyExtractErrorSummary>, ITempPatientPharmacyExtractErrorSummaryRepository
    {
        public TempPatientPharmacyExtractErrorSummaryRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}