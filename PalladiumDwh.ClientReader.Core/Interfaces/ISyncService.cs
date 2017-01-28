using System.Collections.Generic;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.Extract;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface ISyncService
    {
        void SyncPatients<TSource,TDestination>();
    }
}