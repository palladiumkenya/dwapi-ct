using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
{

    public interface IOtzRepository : IRepository<OtzExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<OtzExtract> profileOtzExtracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<OtzExtract> extracts);

        void SyncNew(List<OtzProfile> profiles, IActionRegisterRepository repo);

        void SyncNewPatients(IEnumerable<OtzProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
