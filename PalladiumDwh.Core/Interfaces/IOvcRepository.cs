using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
{

    public interface IOvcRepository : IRepository<OvcExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<OvcExtract> profileOvcExtracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<OvcExtract> extracts);

        void SyncNew(IEnumerable<OvcProfile> profiles);

        void SyncNewPatients(IEnumerable<OvcProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds);
    }
}
