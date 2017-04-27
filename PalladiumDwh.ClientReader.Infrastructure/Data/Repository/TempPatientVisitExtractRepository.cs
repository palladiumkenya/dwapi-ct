using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class TempPatientVisitExtractRepository : TempExtractRepository<TempPatientVisitExtract>, ITempPatientVisitExtractRepository
    {
        public TempPatientVisitExtractRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}