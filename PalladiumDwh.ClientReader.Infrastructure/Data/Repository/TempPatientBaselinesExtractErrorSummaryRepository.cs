using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class TempPatientBaselinesExtractErrorSummaryRepository : TempExtractErrorSummaryRepository<TempPatientBaselinesExtractErrorSummary>, ITempPatientBaselinesExtractErrorSummaryRepository
    {
        public TempPatientBaselinesExtractErrorSummaryRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}