using System;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Repository
{
    public interface IPatientExtractRepository : IRepository<PatientExtract>
    {
        Guid? GetPatientBy(Guid facilityId, string patientNumber);
        Guid? GetPatientBy(Guid facilityId, int patientPID);
        Guid? Sync(PatientExtract patient);
    }
}
