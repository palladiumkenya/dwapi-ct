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
        void Save(DatabaseConfig databaseConfig);
        void SaveEmr(DatabaseConfig databaseConfig);
        Task<bool> CanConnect(string key = "");
        Task<bool> CanConnect(DatabaseConfig databaseConfig);
        DatabaseConfig Read(string key="");
    }
}