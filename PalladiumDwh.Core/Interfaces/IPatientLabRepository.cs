using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientLabRepository : IRepository<PatientLaboratoryExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientLaboratoryExtract> profilePatientLaboratoryExtracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<PatientLaboratoryExtract> extracts);
  }
}
