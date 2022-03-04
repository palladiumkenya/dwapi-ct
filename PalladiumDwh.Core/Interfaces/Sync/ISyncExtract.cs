using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.Core.Interfaces.Sync
{
    public interface ISyncExtract
    {
        Task ProcessPrimary(List<PatientExtractDto> patients);
    }
}
