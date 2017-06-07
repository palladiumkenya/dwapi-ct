using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class TempPatientVisitExtractRepository : TempExtractRepository<TempPatientVisitExtractError>, ITempPatientVisitExtractRepository
    {
        public TempPatientVisitExtractRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}