using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Core.Model.Bag;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Interfaces.Sync
{
    public interface ISyncExtract
    {
        Task ClearFacilitySession(PatientSourceBag patientSourceBag);
        Task ProcessPrimary(PatientSourceBag patientSourceBag);
    }
}
