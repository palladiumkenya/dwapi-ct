using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IArtFastTrackRepository : IRepository<ArtFastTrackExtract>,
        IClearPatientRecords
    {
        void Sync(Guid patientIdValue,
            IEnumerable<ArtFastTrackExtract> profileArtFastTrackExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<ArtFastTrackExtract> extracts);
        void SyncNew(List<ArtFastTrackProfile> profiles, IActionRegisterRepository repo);
        void SyncNewPatients(IEnumerable<ArtFastTrackProfile> profiles,
            IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
