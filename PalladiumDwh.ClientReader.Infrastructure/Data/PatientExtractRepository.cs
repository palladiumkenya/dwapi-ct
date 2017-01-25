using System;
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

        public Guid? Sync(PatientExtract patient)
        {
            var patientId = GetPatientBy(patient.FacilityId, patient.PatientPID);

            if (patientId == Guid.Empty || null == patientId)
            {
                Insert(patient);
                CommitChanges();
                patientId = patient.Id;
            }
            else
            {
                Update(patient);
            }

            return patientId;
        }
    }
}
