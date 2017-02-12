using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model.Source;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Commands
{
    public interface IClearExtractsCommand
    {
        int Execute();
        Task<int> ExecuteAsync(IProgress<int> progress);
    }
}