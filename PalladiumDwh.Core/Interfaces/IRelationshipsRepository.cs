using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IRelationshipsRepository : IRepository<RelationshipsExtract>,
        IClearPatientRecords
    {
        void Sync(Guid patientIdValue,
            IEnumerable<RelationshipsExtract> profileRelationshipsExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<RelationshipsExtract> extracts);
        void SyncNew(List<RelationshipsProfile> profiles, IActionRegisterRepository repo);
        void SyncNewPatients(IEnumerable<RelationshipsProfile> profiles,
            IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
