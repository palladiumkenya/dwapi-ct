using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
{

    public interface IDepressionScreeningRepository : IRepository<DepressionScreeningExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<DepressionScreeningExtract> profileDepressionScreeningExtracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<DepressionScreeningExtract> extracts);

        void SyncNew(IEnumerable<DepressionScreeningProfile> profiles);

        void SyncNewPatients(IEnumerable<DepressionScreeningProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds);
    }
}
