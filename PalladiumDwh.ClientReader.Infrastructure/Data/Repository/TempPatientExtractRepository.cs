using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class TempPatientExtractRepository:TempExtractRepository<TempPatientExtractError>, ITempPatientExtractRepository
    {
        public TempPatientExtractRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}