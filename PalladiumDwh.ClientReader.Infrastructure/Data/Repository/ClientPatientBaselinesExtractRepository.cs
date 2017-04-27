using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class ClientPatientBaselinesExtractRepository : ClientExtractRepository<ClientPatientBaselinesExtract>, IClientPatientBaselinesExtractRepository
    {
        public ClientPatientBaselinesExtractRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}