using System;
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
        public void Clear(Guid patientId)
        {
            DeleteBy(x => x.PatientId == patientId);
        }
    }
}
