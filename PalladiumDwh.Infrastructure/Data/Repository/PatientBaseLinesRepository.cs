
using System;
using System.Collections.Generic;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Data.Repository;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
    public class PatientBaseLinesRepository : GenericRepository<PatientBaselinesExtract>, IPatientBaseLinesRepository
    {
        private readonly DwapiCentralContext _context;
        public PatientBaseLinesRepository(DwapiCentralContext context) : base(context)
        {
            _context = context;
        }
        public void Clear(Guid patientId)
        {
            DeleteBy(x => x.PatientId == patientId);
        }
        public void Sync(Guid patientId, IEnumerable<PatientBaselinesExtract> extracts)
        {
            Clear(patientId);
            Insert(extracts);
            CommitChanges();
        }
    }
}
