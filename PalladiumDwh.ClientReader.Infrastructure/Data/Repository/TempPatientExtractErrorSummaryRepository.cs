using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class TempPatientExtractErrorSummaryRepository : TempExtractErrorSummaryRepository<TempPatientExtractErrorSummary>, ITempPatientExtractErrorSummaryRepository
    {
        public TempPatientExtractErrorSummaryRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}