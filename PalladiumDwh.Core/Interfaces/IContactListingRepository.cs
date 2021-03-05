using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
{

    public interface IContactListingRepository : IRepository<ContactListingExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<ContactListingExtract> profileContactListingExtracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<ContactListingExtract> extracts);

        void SyncNew(List<ContactListingProfile> profiles, IActionRegisterRepository repo);

        void SyncNewPatients(IEnumerable<ContactListingProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
