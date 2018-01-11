
using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientArtExtractRepository : IRepository<PatientArtExtract>,IClearPatientRecords
    {
        void Sync(Guid patientId,IEnumerable<PatientArtExtract> extracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<PatientArtExtract> extracts);
        void SyncNew(IEnumerable<PatientARTProfile> profiles);

        void SyncNewPatients(IEnumerable<PatientARTProfile> profiles, IFacilityRepository facilityRepository);
    }
}
