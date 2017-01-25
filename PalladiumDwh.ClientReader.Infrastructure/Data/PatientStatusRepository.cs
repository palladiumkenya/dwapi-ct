using System;
using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.Shared.Data.Repository;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data
{
  
    public class PatientStatusRepository : GenericRepository<PatientStatusExtract>, IPatientStatusRepository
    {
        private readonly DwapiRemoteContext _context;
        public PatientStatusRepository(DwapiRemoteContext context) : base(context)
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