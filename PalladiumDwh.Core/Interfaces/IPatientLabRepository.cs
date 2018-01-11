using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientLabRepository : IRepository<PatientLaboratoryExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientLaboratoryExtract> profilePatientLaboratoryExtracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<PatientLaboratoryExtract> extracts);
        void SyncNew(IEnumerable<PatientLabProfile> profiles);

        void SyncNewPatients(IEnumerable<PatientLabProfile> profiles, IFacilityRepository facilityRepository);
    }
}
