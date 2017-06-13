using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Commands
{
    public interface IAnalyzeTempExtractsCommand
    {
        Task<IEnumerable<EventHistory>> ExecuteAsync(EMR emr, IProgress<DProgress> progress = null);
    }
}