
using System;
using System.Linq.Expressions;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
    public class PatientArtExtractRepository : GenericRepository<PatientArtExtract>, IPatientArtExtractRepository
    {
        private readonly DwhServerContext _context;
        public PatientArtExtractRepository(DwhServerContext context) : base(context)
        {
            _context = context;
        }

        public void Clear(Guid patientId)
        {
            DeleteBy(x=>x.PatientId==patientId);
        }
    }

}
