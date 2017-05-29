
using System;
using System.Threading.Tasks;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientExtractRepository : IRepository<PatientExtract>
    {
        Guid? GetPatientBy(Guid facilityId, string patientNumber);
        Guid? GetPatientBy(Guid facilityId, int patientPID);
        Guid? Sync(PatientExtract patient);
        Task ClearManifest(Manifest manifest);
        Task<MasterFacility> VerifyFacility(int siteCode);
    }
}
