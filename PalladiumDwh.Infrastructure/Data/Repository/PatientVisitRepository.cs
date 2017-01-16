using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
    public class PatientVisitRepository : GenericRepository<PatientVisitExtract>, IPatientVisitRepository
    {
        private readonly DwhServerContext _context;
        public PatientVisitRepository(DwhServerContext context) : base(context)
        {
            _context = context;
        }
    }
}