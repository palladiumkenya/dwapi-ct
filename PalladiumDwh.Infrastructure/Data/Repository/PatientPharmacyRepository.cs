
using System;
using System.Collections.Generic;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
    public class PatientPharmacyRepository : GenericRepository<PatientPharmacyExtract>,IPatientPharmacyRepository
    {
        private readonly DwapiCentralContext _context;
        public PatientPharmacyRepository(DwapiCentralContext context) : base(context)
        {
            _context = context;
        }

        public void Clear(Guid patientId)
        {
            DeleteBy(x => x.PatientId == patientId);
        }
        public void Sync(Guid patientId, IEnumerable<PatientPharmacyExtract> extracts)
        {
            Clear(patientId);
            Insert(extracts);
            CommitChanges();
        }
    }
}
