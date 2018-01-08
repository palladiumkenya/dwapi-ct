using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientVisitRepository : IRepository<PatientVisitExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientVisitExtract> profilePatientVisitExtracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<PatientVisitExtract> extracts);
  }
}