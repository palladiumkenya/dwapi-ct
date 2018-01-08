﻿
using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientPharmacyRepository : IRepository<PatientPharmacyExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientPharmacyExtract> profilePatientPharmacyExtracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<PatientPharmacyExtract> extracts);
  }
}
