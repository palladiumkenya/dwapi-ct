using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
{

    public interface IGbvScreeningRepository : IRepository<GbvScreeningExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<GbvScreeningExtract> profileGbvScreeningExtracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<GbvScreeningExtract> extracts);

        void SyncNew(List<GbvScreeningProfile> profiles, IActionRegisterRepository repo);

        void SyncNewPatients(IEnumerable<GbvScreeningProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
