using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.Shared.Data.Repository;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.ClientReader.Infrastructure.Data
{
  
    public class PatientStatusRepository : GenericRepository<PatientStatusExtract>, IPatientStatusRepository
    {
        private readonly DwapiRemoteContext _context;
        public PatientStatusRepository(DwapiRemoteContext context) : base(context)
        {
            _context = context;
        }
    }
}