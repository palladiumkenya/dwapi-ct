using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class TempPatientLaboratoryExtractErrorSummaryRepository : TempExtractErrorSummaryRepository<TempPatientLaboratoryExtractErrorSummary>, ITempPatientLaboratoryExtractErrorSummaryRepository
    {
        public TempPatientLaboratoryExtractErrorSummaryRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}