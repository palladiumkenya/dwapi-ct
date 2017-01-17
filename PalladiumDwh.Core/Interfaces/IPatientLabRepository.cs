using System;
using System.Collections.Generic;
using PalladiumDwh.Core.Model;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientLabRepository : IRepository<PatientLaboratoryExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientLaboratoryExtract> profilePatientLaboratoryExtracts);
    }
}
