using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IDatabaseManager
    {
        string DatabaseName { get; }
        bool CheckDatabaseExist(string provider, string connectionString);
        Task RunUpdateAsync(IProgress<DProgress> progress=null);
        IDbConnection GetConnection(string provider, string connectionString);
        DatabaseConfig GetDatabaseConfig(string provider, string connectionString);
        Task<bool> CheckConnection(DatabaseConfig databaseConfig);
        Task<List<string>> GetServersList(DatabaseConfig databaseConfig,IProgress<DProgress> progress = null);
        Task<List<string>> GetDatabaseList(DatabaseConfig databaseConfig, IProgress<DProgress> progress = null);
    }
}