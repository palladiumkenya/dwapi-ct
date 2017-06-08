using System;
using System.Collections.Generic;
using System.Configuration;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Core.Services
{
    public class DatabaseSetupService:IDatabaseSetupService
    {
        private readonly string _localKey= "DWAPIRemote";
        private readonly string _databaseConfigFile;

        public DatabaseSetupService(string databaseConfigFile)
        {
            _databaseConfigFile = databaseConfigFile;
            ConnectionStringSettingses =GetAll(ConfigurationManager.ConnectionStrings);
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
    }
}