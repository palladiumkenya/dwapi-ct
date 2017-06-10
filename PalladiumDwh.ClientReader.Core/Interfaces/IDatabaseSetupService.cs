using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IDatabaseSetupService
    {
        List<ConnectionStringSettings> ConnectionStringSettingses { get; set; }
        void Refresh();
        Task Save(DatabaseConfig databaseConfig);
        Task SaveEmr(DatabaseConfig databaseConfig);
        Task<bool> ServerExists(string key = "");
        Task<bool> ServerExists(DatabaseConfig databaseConfig);
        Task<bool> DatabaseExists(DatabaseConfig databaseConfig, IProgress<DProgress> progress = null);

        Task<bool> CanConnect(string key = "");
        Task<bool> CanConnect(DatabaseConfig databaseConfig);
        DatabaseConfig Read(string key="");
    }
}