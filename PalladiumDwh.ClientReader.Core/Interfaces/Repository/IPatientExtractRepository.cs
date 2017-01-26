using System;
using System.Collections.Generic;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Repository
{
    public interface IPatientExtractRepository : IRepository<PatientExtract>
    {
        Guid? GetPatientBy(Guid facilityId, int patientPk);
        IEnumerable<PatientExtract> Sync(IEnumerable<PatientExtract> patientExtracts);
    }
}
