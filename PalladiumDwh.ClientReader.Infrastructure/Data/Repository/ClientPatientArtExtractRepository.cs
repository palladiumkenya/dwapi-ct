using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class ClientPatientArtExtractRepository : ClientExtractRepository<ClientPatientArtExtract>, IClientPatientArtExtractRepository
    {
        public ClientPatientArtExtractRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}