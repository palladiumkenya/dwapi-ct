using System;
using System.Data;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IDatabaseManager
    {
        string DatabaseName { get; }
        bool CheckDatabaseExist();
        Task RunUpdateAsync(IProgress<DProgress> progress=null);
        IDbConnection GetConnection(string provider, string connectionString);
        DatabaseConfig GetDatabaseConfig(string provider, string connectionString);
    }
}