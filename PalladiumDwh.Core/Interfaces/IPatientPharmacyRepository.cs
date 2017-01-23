
using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientPharmacyRepository : IRepository<PatientPharmacyExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientPharmacyExtract> profilePatientPharmacyExtracts);
    }
}
