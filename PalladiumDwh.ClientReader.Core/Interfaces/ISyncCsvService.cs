using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface ISyncCsvService
    {
        Task<int> InitializeAsync(List<string> csvFiles,IProgress<DProgress> dprogress = null);
        Task<RunSummary> SyncAsync(ExtractSetting extract,string extractCsv, Progress<ProcessStatus> progress = null, Progress<DProgress> dprogress = null);
    }
}