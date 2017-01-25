using System;
using System.Collections.Generic;
using System.Linq;
using PalladiumDwh.ClientReader.Core.Interfaces.Repository;
using PalladiumDwh.Shared.Data.Repository;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data
{
    public class PatientExtractRepository : GenericRepository<PatientExtract>, IPatientExtractRepository
    {
        
        private readonly DwapiRemoteContext _context;

        public PatientExtractRepository(DwapiRemoteContext context) : base(context)
        {
            _context = context;
        }

        public Guid? GetPatientBy(Guid facilityId, int patientPk)
        {
            return Find(
                x => x.FacilityId == facilityId &&
                     x.PatientPID == patientPk
            )?.Id;
        }

        public IEnumerable<PatientExtract> Sync(IEnumerable<PatientExtract> patientExtracts)
        {
            var toSave = patientExtracts.ToList();
            Insert(toSave);
            CommitChanges();
            return toSave;
        }
    }
}
