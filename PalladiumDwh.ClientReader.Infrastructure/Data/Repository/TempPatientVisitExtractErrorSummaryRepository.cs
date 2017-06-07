using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class TempPatientVisitExtractErrorSummaryRepository : TempExtractErrorSummaryRepository<TempPatientVisitExtractErrorSummary>, ITempPatientVisitExtractErrorSummaryRepository
    {
        public TempPatientVisitExtractErrorSummaryRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}