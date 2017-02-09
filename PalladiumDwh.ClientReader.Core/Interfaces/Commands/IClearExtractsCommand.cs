using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Commands
{
    public interface IClearExtractsCommand
    {
        void Execute();
        Task ExecuteAsync();
    }
}