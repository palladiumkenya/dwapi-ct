using System;
using System.Collections.Generic;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Data.Repository;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
  
    public class PatientStatusRepository : GenericRepository<PatientStatusExtract>, IPatientStatusRepository
    {
        private readonly DwapiCentralContext _context;
        public PatientStatusRepository(DwapiCentralContext context) : base(context)
        {
            _context = context;
        }
        public void Clear(Guid patientId)
        {
            DeleteBy(x => x.PatientId == patientId);
        }
        public void Sync(Guid patientId, IEnumerable<PatientStatusExtract> extracts)
        {
            Clear(patientId);
            Insert(extracts);
            CommitChanges();
        }
    }
}