﻿
using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;
using PalladiumDwh.Shared.Model.Profile;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientBaseLinesRepository : IRepository<PatientBaselinesExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientBaselinesExtract> patientArtExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<PatientBaselinesExtract> extracts);
        void SyncNew(IEnumerable<PatientBaselineProfile> profiles);

        void SyncNewPatients(IEnumerable<PatientBaselineProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds);
    }
}
