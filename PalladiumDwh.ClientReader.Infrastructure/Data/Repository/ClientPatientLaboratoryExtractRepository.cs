using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class ClientPatientLaboratoryExtractRepository : ClientExtractRepository<ClientPatientLaboratoryExtract>,
        IClientPatientLaboratoryExtractRepository
    {
        public ClientPatientLaboratoryExtractRepository(DwapiRemoteContext context) : base(context)
        {
        }
    }
}