
using System;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
    public class PatientPharmacyRepository : GenericRepository<PatientPharmacyExtract>,IPatientPharmacyRepository
    {
        private readonly DwhServerContext _context;
        public PatientPharmacyRepository(DwhServerContext context) : base(context)
        {
            _context = context;
        }

        public void Clear(Guid patientId)
        {
            DeleteBy(x => x.PatientId == patientId);
        }
    }
}
