using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
{
  
    public interface IPatientStatusRepository : IRepository<PatientStatusExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientStatusExtract> profilePatientStatusExtracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<PatientStatusExtract> extracts);

        void SyncNew(IEnumerable<PatientStatusProfile> profiles);

        void SyncNewPatients(IEnumerable<PatientStatusProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds);
    }
}