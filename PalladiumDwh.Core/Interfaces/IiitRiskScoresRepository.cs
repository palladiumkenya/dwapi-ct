using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IIITRiskScoresRepository : IRepository<IITRiskScoresExtract>,
        IClearPatientRecords
    {
        void Sync(Guid patientIdValue,
            IEnumerable<IITRiskScoresExtract> profileIITRiskScoresExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<IITRiskScoresExtract> extracts);
        void SyncNew(List<IITRiskScoresProfile> profiles, IActionRegisterRepository repo);
        void SyncNewPatients(IEnumerable<IITRiskScoresProfile> profiles,
            IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
