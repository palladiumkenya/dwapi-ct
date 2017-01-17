using System;
using System.Collections.Generic;
using PalladiumDwh.Core.Model;

namespace PalladiumDwh.Core.Interfaces
{
  
    public interface IPatientStatusRepository : IRepository<PatientStatusExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientStatusExtract> profilePatientStatusExtracts);
    }
}