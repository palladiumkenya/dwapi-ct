using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class ClientPatientExtractRepository:ClientExtractRepository<ClientPatientExtract>, IClientPatientExtractRepository
    {
        public ClientPatientExtractRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}