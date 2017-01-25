using System;
using System.Collections.Generic;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.Shared.Data.Repository;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data
{
    public class PatientVisitRepository : GenericRepository<PatientVisitExtract>, IPatientVisitRepository
    {
        private readonly DwapiRemoteContext _context;
        public PatientVisitRepository(DwapiRemoteContext context) : base(context)
        {
            _context = context;
        }
        public void Clear(Guid patientId)
        {
            DeleteBy(x => x.PatientId == patientId);
        }
        public void Sync(Guid patientId, IEnumerable<PatientVisitExtract> extracts)
        {
            Clear(patientId);
            Insert(extracts);
            CommitChanges();
        }
    }
}