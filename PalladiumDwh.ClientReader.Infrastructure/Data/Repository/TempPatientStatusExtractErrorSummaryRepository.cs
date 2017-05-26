using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class TempPatientStatusExtractErrorSummaryRepository : TempExtractErrorSummaryRepository<TempPatientStatusExtractErrorSummary>, ITempPatientStatusExtractErrorSummaryRepository
    {
        public TempPatientStatusExtractErrorSummaryRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}