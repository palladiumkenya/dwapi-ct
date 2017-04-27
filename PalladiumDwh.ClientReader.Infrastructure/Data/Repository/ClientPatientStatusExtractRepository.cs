using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class ClientPatientStatusExtractRepository : ClientExtractRepository<ClientPatientStatusExtract>, IClientPatientStatusExtractRepository
    {
        public ClientPatientStatusExtractRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}