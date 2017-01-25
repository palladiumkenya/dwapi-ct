
using System;
using System.Collections.Generic;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Data.Repository;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
    public class PatientArtExtractRepository : GenericRepository<PatientArtExtract>, IPatientArtExtractRepository
    {
        private readonly DwapiCentralContext _context;
        public PatientArtExtractRepository(DwapiCentralContext context) : base(context)
        {
            _context = context;
        }

        public void Clear(Guid patientId)
        {
            DeleteBy(x => x.PatientId == patientId);
        }

        public void Sync(Guid patientId, IEnumerable<PatientArtExtract> extracts)
        {
            Clear(patientId);
            Insert(extracts);
            CommitChanges();
        }
    }

}
