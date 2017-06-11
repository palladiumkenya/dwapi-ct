using System;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface ISyncCsvService
    {
        Task<int> InitializeAsync();
        Task<RunSummary> SyncAsync(ExtractSetting extract,string extractCsv, Progress<ProcessStatus> progress = null, Progress<DProgress> dprogress = null);
    }
}