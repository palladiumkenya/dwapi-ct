using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Core.Services
{
    public class DatabaseSetupService:IDatabaseSetupService
    {
        private readonly string _localKey= "DWAPIRemote";
        private readonly IDatabaseManager _databaseManager;

        public DatabaseSetupService(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
            ConnectionStringSettingses = GetAll(ConfigurationManager.ConnectionStrings);
        }

        private List<ConnectionStringSettings> GetAll(ConnectionStringSettingsCollection connectionStrings)
        {
            List < ConnectionStringSettings> list=new List<ConnectionStringSettings>();


            foreach (ConnectionStringSettings c in connectionStrings)
            {
                list.Add(c);
            }
            return list;
        }

        public List<ConnectionStringSettings> ConnectionStringSettingses { get; set; }=new List<ConnectionStringSettings>();

        public void Refresh()
        {
            ConnectionStringSettingses = GetAll(ConfigurationManager.ConnectionStrings);
        }

        public void Save(DatabaseConfig databaseConfig)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings[$"{_localKey}"].ConnectionString = databaseConfig.GetConnectionString();
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        public void SaveEmr(DatabaseConfig databaseConfig)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings[$"{databaseConfig.DatabaseType.Key}"].ConnectionString = databaseConfig.GetConnectionString();
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        public DatabaseConfig Read(string key)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            var connectionString = connectionStringsSection.ConnectionStrings[$"{key}"].ConnectionString;
            var dbtype=DatabaseType.GetAll().FirstOrDefault(x => x.Key.ToLower() == key);

            var databaseConfig=_databaseManager.GetDatabaseConfig(dbtype.Provider, connectionString);
            databaseConfig.DatabaseType = dbtype;
            return databaseConfig;
        }
    }
}