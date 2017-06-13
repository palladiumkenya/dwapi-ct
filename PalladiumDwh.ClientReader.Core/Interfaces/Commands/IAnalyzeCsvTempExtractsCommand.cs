using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Commands
{
    public interface IAnalyzeCsvTempExtractsCommand
    {
        Task<IEnumerable<EventHistory>> ExecuteAsync(EMR emr, List<string> csvFiles, IProgress<DProgress> progress = null);
        IEnumerable<EventHistory> GenerateFoundEventHistory(string csvFile);
    }
}