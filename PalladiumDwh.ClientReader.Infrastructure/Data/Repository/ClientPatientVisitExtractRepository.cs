using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class ClientPatientVisitExtractRepository : ClientExtractRepository<ClientPatientVisitExtract>, IClientPatientVisitExtractRepository
    {
        public ClientPatientVisitExtractRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}