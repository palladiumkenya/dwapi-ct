using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
{

    public interface IDefaulterTracingRepository : IRepository<DefaulterTracingExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<DefaulterTracingExtract> profileDefaulterTracingExtracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<DefaulterTracingExtract> extracts);

        void SyncNew(List<DefaulterTracingProfile> profiles, IActionRegisterRepository repo);

        void SyncNewPatients(IEnumerable<DefaulterTracingProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
