using System;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Commands
{
    public interface ILoadCsvExtractCommand<T> where T: TempExtract
    {
        LoadSummary Summary { get; }
        Task<LoadSummary> ExecuteAsync(string csvExtract, Progress<DProgress> progress = null);
    }
}