using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
{

    public interface IAllergiesChronicIllnessRepository : IRepository<AllergiesChronicIllnessExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<AllergiesChronicIllnessExtract> profileAllergiesChronicIllnessExtracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<AllergiesChronicIllnessExtract> extracts);

        void SyncNew(IEnumerable<AllergiesChronicIllnessProfile> profiles);

        void SyncNewPatients(IEnumerable<AllergiesChronicIllnessProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds);
    }
}
