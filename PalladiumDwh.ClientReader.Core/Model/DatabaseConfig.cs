using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.ClientReader.Core.Model
{
    public class DatabaseConfig
    {
        public DatabaseType DatabaseType { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string AdvancedProperties { get; set; }

        public string GetConnectionString()
        {
            string connectionString = string.Empty;

            var dwapiSetting = DefaultSettings.ProviderSettings.FirstOrDefault(x => x.Name == DatabaseType.Provider);

            if (null != dwapiSetting)
            {
                if (!string.IsNullOrWhiteSpace(dwapiSetting.Value))
                {
                    AdvancedProperties = dwapiSetting.Value.HasToEndsWith(";");
                }
            }

            if (DatabaseType.Provider.ToLower().Contains("System.Data.SqlClient".ToLower()))
            {
                connectionString = $@"Data Source={Server};Initial Catalog={Database};User ID={User};Password={Password};";
            }
            if (DatabaseType.Provider.ToLower().Contains("MySql.Data.MySqlClient".ToLower()))
            {
                Port = Port > 0 ? Port : 3306;
                connectionString = $@"Server={Server};Port={Port};Database={Database};Uid={User};Pwd={Password};";
            }
            if (DatabaseType.Provider.ToLower().Contains("Npgsql".ToLower()))
            {
                Port = Port > 0 ? Port : 5432;
                connectionString = $@"Server={Server};Port={Port};Database={Database};User Id={User};Password={Password};";
            }

            connectionString = connectionString.HasToEndsWith(";");

            connectionString = string.IsNullOrWhiteSpace(AdvancedProperties)
                ? $"{connectionString}"
                : $"{connectionString}{AdvancedProperties}";

            return connectionString;
        }

    }
}
