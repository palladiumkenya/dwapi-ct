using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class ClientPatientPharmacyExtractRepository : ClientExtractRepository<ClientPatientPharmacyExtract>, IClientPatientPharmacyExtractRepository
    {
        public ClientPatientPharmacyExtractRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}