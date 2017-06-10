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
        void Execute();
        Task<LoadSummary> ExecuteAsync(Progress<ProcessStatus> progressPercent = null);
        Task<LoadSummary> ExecuteLoadAsync(Progress<DProgress> progressPercent = null);
    }
}