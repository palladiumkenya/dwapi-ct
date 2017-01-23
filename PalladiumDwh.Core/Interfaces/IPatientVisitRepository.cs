using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientVisitRepository : IRepository<PatientVisitExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientVisitExtract> profilePatientVisitExtracts);
    }
}