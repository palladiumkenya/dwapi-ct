using System.Threading.Tasks;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Commands
{
    public interface IClearExtractsCommand
    {
        int Execute();
        Task<int> ExecuteAsync();
    }
}