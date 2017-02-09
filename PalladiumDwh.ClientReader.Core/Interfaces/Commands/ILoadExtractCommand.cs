using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Commands
{
    public interface ILoadExtractCommand<T> where T: TempExtract
    {
        LoadSummary Summary { get; }
        void Execute();
        Task<LoadSummary> ExecuteAsync();
    }
}