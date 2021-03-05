using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
{

    public interface IIptRepository : IRepository<IptExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<IptExtract> profileIptExtracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<IptExtract> extracts);

        void SyncNew(List<IptProfile> profiles, IActionRegisterRepository repo);

        void SyncNewPatients(IEnumerable<IptProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
