using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.Shared.Data.Repository;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.ClientReader.Infrastructure.Data
{
    public class PatientVisitRepository : GenericRepository<PatientVisitExtract>, IPatientVisitRepository
    {
        private readonly DwapiRemoteContext _context;
        public PatientVisitRepository(DwapiRemoteContext context) : base(context)
        {
            _context = context;
        }
    }
}