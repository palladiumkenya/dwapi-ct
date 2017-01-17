
using System;
using PalladiumDwh.Core.Interfaces;
using PalladiumDwh.Core.Model;

namespace PalladiumDwh.Infrastructure.Data.Repository
{
    public class PatientExtractRepository : GenericRepository<PatientExtract>, IPatientExtractRepository
    {
        private readonly DwhServerContext _context;

        public PatientExtractRepository(DwhServerContext context) : base(context)
        {
            _context = context;
        }

        public Guid? GetPatientBy(Guid facilityId, string patientNumber)
        {
            return Find(
                x => x.FacilityId == facilityId &&
                     x.PatientCccNumber.ToLower() == patientNumber.ToLower()
            )?.Id;
        }

        public Guid? GetPatientBy(Guid facilityId, int patientPID)
        {
            return Find(
               x => x.FacilityId == facilityId &&
                    x.PatientPID == patientPID
           )?.Id;
        }
    }
}
