using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Commands
{
    public interface ISyncCommand<TSource,TDestination> where TSource : TempExtract where TDestination : ClientExtract
    {
        void Execute();
    }
}