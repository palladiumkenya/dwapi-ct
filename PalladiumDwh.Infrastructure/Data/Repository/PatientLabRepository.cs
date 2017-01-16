using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
    public class PatientLabRepository : GenericRepository<PatientLaboratoryExtract>, IPatientLabRepository
    {
        private readonly DwhServerContext _context;
        public PatientLabRepository(DwhServerContext context) : base(context)
        {
            _context = context;
        }
    }
}
