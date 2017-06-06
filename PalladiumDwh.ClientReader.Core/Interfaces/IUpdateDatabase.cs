using System;
using System.Threading.Tasks;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IUpdateDatabase
    {
        Task RunUpdateAsync(IProgress<DProgress> progress=null);
    }
}