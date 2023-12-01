using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
{
    public interface ICervicalCancerScreeningRepository : IRepository<CervicalCancerScreeningExtract>,
        IClearPatientRecords
    {
        void Sync(Guid patientIdValue,
            IEnumerable<CervicalCancerScreeningExtract> profileCervicalCancerScreeningExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<CervicalCancerScreeningExtract> extracts);
        void SyncNew(List<CervicalCancerScreeningProfile> profiles, IActionRegisterRepository repo);
        void SyncNewPatients(IEnumerable<CervicalCancerScreeningProfile> profiles,
            IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}