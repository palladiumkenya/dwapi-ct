using System.Collections.Generic;
using System.Configuration;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IDatabaseSetupService
    {
        List<ConnectionStringSettings> ConnectionStringSettingses { get; set; }
        void Refresh();
        void Save(DatabaseConfig databaseConfig);
        void SaveEmr(DatabaseConfig databaseConfig);
    }
}