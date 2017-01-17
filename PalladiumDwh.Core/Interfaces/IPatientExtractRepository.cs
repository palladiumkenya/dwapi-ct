
using System;
using PalladiumDwh.Core.Model;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientExtractRepository : IRepository<PatientExtract>
    {
        Guid? GetPatientBy(Guid facilityId, string patientNumber);
        Guid? GetPatientBy(Guid facilityId, int patientPID);
    }
}
