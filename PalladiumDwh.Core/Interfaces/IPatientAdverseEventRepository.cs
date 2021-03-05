using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientAdverseEventRepository : IRepository<PatientAdverseEventExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientAdverseEventExtract> profilePatientStatusExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<PatientAdverseEventExtract> extracts);

        void SyncNew(List<PatientAdverseEventProfile> profiles, IActionRegisterRepository actionRegisterRepository);

        void SyncNewPatients(IEnumerable<PatientAdverseEventProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository actionRegisterRepository);
    }
}