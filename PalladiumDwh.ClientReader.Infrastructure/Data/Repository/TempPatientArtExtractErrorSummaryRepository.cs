using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class TempPatientArtExtractErrorSummaryRepository : TempExtractErrorSummaryRepository<TempPatientArtExtractErrorSummary>, ITempPatientArtExtractErrorSummaryRepository
    {
        public TempPatientArtExtractErrorSummaryRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}