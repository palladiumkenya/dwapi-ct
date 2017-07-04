using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using log4net;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Services
{
    public class DatabaseSetupService : IDatabaseSetupService
    {
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string _localKey = "DWAPIRemote";
        private readonly IDatabaseManager _databaseManager;

        public DatabaseSetupService(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
            ConnectionStringSettingses = GetAll(ConfigurationManager.ConnectionStrings);
        }

        private List<ConnectionStringSettings> GetAll(ConnectionStringSettingsCollection connectionStrings)
        {
            List<ConnectionStringSettings> list = new List<ConnectionStringSettings>();


            foreach (ConnectionStringSettings c in connectionStrings)
            {
                list.Add(c);
            }
            return list;
        }

        public List<ConnectionStringSettings> ConnectionStringSettingses { get; set; } =
            new List<ConnectionStringSettings>();

        public void Refresh()
        {
            ConnectionStringSettingses = GetAll(ConfigurationManager.ConnectionStrings);
        }

        public async Task Save(DatabaseConfig databaseConfig)
        {
            Log.Debug($"Saving database settings...");
            Configuration config;
            try
            {
                Log.Debug($"reading exisitng settings...");
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                Log.Debug($"Changing from:");
                Log.Debug(connectionStringsSection.ConnectionStrings[$"{_localKey}"].ConnectionString);
                Log.Debug($"To:");
                Log.Debug(databaseConfig.GetConnectionString());
                connectionStringsSection.ConnectionStrings[$"{_localKey}"].ConnectionString =
                    databaseConfig.GetConnectionString();

            }
            catch (Exception e)
            {
                Log.Debug($"Changing database settings Failed!");
                Log.Debug(e);
                throw;
            }
            
            await Task.Run(() =>
            {
                try
                {
                    Log.Debug($"Saving database settings...");
                    config.Save(ConfigurationSaveMode.Modified,false);
                    //ConfigurationManager.RefreshSection("connectionStrings");
                }
                catch(Exception e)
                {
                    Log.Debug($"Saving database settings failed!");
                    Log.Debug(e);
                    throw;
                }
                
            });
        }

        public async Task SaveEmr(DatabaseConfig databaseConfig)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection) config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings[$"{databaseConfig.DatabaseType.Key}"].ConnectionString =
                databaseConfig.GetConnectionString();
            await Task.Run(() =>
            {
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
            });
        }

        public async Task<bool> ServerExists(string key = "")
        {
            var databaseConfig = Read(key);
            return await _databaseManager.CheckServerConnection(databaseConfig);
        }

        public async Task<bool> ServerExists(DatabaseConfig databaseConfig)
        {
            return await _databaseManager.CheckServerConnection(databaseConfig);
        }

        public async Task<bool> DatabaseExists(DatabaseConfig databaseConfig, IProgress<DProgress> progress = null)
        {
            return await _databaseManager.CheckAppConnection(databaseConfig, progress);
        }

        public async Task<bool> CanConnect(string key = "")
        {
            var databaseConfig = Read(key);
            return await _databaseManager.CheckConnection(databaseConfig);
        }

        public async Task<bool> CanConnect(DatabaseConfig databaseConfig)
        {
            return await _databaseManager.CheckConnection(databaseConfig);
        }

        public DatabaseConfig Read(string key = "")
        {
            key = string.IsNullOrWhiteSpace(key) ? _localKey : key;

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection) config.GetSection("connectionStrings");
            var connectionString = connectionStringsSection.ConnectionStrings[$"{key}"].ConnectionString;
            var provider = connectionStringsSection.ConnectionStrings[$"{key}"].ProviderName;

            var dbtype = DatabaseType.GetAll().FirstOrDefault(x => x.Provider.ToLower() == provider.ToLower());

            var databaseConfig = _databaseManager.GetDatabaseConfig(dbtype.Provider, connectionString);
            databaseConfig.DatabaseType = dbtype;
            return databaseConfig;
        }
    }
}