using System;
using System.Collections.Generic;
using PalladiumDwh.Core.Model;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientVisitRepository : IRepository<PatientVisitExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientVisitExtract> profilePatientVisitExtracts);
    }
}