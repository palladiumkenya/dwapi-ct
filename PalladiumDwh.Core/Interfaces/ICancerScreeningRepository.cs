using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
{
    public interface ICancerScreeningRepository : IRepository<CancerScreeningExtract>,
        IClearPatientRecords
    {
        void Sync(Guid patientIdValue,
            IEnumerable<CancerScreeningExtract> profileCancerScreeningExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<CancerScreeningExtract> extracts);
        void SyncNew(List<CancerScreeningProfile> profiles, IActionRegisterRepository repo);
        void SyncNewPatients(IEnumerable<CancerScreeningProfile> profiles,
            IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
