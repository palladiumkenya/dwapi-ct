using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class TempPatientBaselinesExtractRepository : TempExtractRepository<TempPatientBaselinesExtract>, ITempPatientBaselinesExtractRepository
    {
        public TempPatientBaselinesExtractRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}