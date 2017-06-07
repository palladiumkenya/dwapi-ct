using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalladiumDwh.ClientReader.Core
{
    public class DatabaseType
    {
        public string Name { get; set; }
        public string Provider { get; set; }
        public string Key { get; set; }
        public int DefaultPort { get; set; }
        public bool Default { get; set; }

        public DatabaseType(string name, string provider, string key, int defaultPort,bool defaultDb=false)
        {
            Name = name;
            Provider = provider;
            Key = key;
            Default = defaultDb;
            DefaultPort = defaultPort;
        }

        public static List<DatabaseType> GetAll()
        {
            return new List<DatabaseType>
            {
                new DatabaseType("SQL Server","System.Data.SqlClient","EMRDatabase",1433,true),
                new DatabaseType("MySQL","MySql.Data.MySqlClient","MySQLEMRDatabase",3306),
                new DatabaseType("Postgre","Npgsql","PostgreSQLEMRDatabase",5432)
            };
        }
    }
}
