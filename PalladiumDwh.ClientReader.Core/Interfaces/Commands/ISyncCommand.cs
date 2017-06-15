using System;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Model.Source;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Commands
{
    public interface ISyncCommand<TSource,TDestination> where TSource : TempExtract where TDestination : ClientExtract
    {
        SyncSummary Summary { get; }
        void Execute();
        Task<SyncSummary> ExecuteAsync(IProgress<DProgress> progress = null);
    }
}