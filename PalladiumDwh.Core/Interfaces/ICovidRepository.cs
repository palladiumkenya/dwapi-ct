using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
{

    public interface ICovidRepository : IRepository<CovidExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<CovidExtract> profileCovidExtracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<CovidExtract> extracts);

        void SyncNew(List<CovidProfile> profiles, IActionRegisterRepository repo);

        void SyncNewPatients(IEnumerable<CovidProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
