using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class TempPatientStatusExtractRepository : TempExtractRepository<TempPatientStatusExtract>, ITempPatientStatusExtractRepository
    {
        public TempPatientStatusExtractRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}