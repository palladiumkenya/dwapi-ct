using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model;
using System.Linq;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Repository
{
    public class ClientPatientExtractRepository:ClientExtractRepository<ClientPatientExtract>, IClientPatientExtractRepository
    {
        public ClientPatientExtractRepository(DwapiRemoteContext context) : base(context)
        {
        }

      
    }
}