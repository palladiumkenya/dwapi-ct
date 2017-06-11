using System.Threading.Tasks;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Commands
{
    public interface IClearCsvExtractsCommand
    {
        int Execute();
        Task<int> ExecuteAsync();
    }
}