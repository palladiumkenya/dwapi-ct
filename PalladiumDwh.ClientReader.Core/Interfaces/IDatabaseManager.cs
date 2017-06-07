using System;
using System.Threading.Tasks;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IDatabaseManager
    {
        string DatabaseName { get; }
        bool CheckDatabaseExist();
        Task RunUpdateAsync(IProgress<DProgress> progress=null);
    }
}