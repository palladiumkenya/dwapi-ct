
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
    public class PatientExtractRepository : GenericRepository<PatientExtract>, IPatientExtractRepository
    {
        private readonly DwhServerContext _context;
        public PatientExtractRepository(DwhServerContext context) : base(context)
        {
            _context = context;
        }
    }
}
