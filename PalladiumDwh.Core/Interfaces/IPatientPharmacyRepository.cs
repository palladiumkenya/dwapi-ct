
using System;
using System.Collections.Generic;
using PalladiumDwh.Core.Model;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientPharmacyRepository : IRepository<PatientPharmacyExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientPharmacyExtract> profilePatientPharmacyExtracts);
    }
}
