using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Commands
{
    public interface IValidateExtractCommand<T> where T: TempExtract
    {
        ValidationSummary Summary { get; }
        
        Task<ValidationSummary> ExecuteAsync(Progress<ProcessStatus> progressPercent = null);
    }
}