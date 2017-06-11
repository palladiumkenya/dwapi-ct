using System;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Commands
{
    public interface IValidateExtractCommand<T> where T: TempExtract
    {
        ValidationSummary Summary { get; }
        
        Task<ValidationSummary> ExecuteAsync(Progress<ProcessStatus> progressPercent = null);
        Task<ValidationSummary> ExecuteValidateAsync(Progress<DProgress> progressPercent = null);
    }
}