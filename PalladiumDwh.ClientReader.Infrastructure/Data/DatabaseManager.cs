﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Migrations;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Xsl;
using log4net;
using MySql.Data.MySqlClient;
using Npgsql;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Infrastructure.Migrations;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data
{
    public class DatabaseManager : IDatabaseManager
    {
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly DwapiRemoteContext _context;

        public DatabaseManager(DwapiRemoteContext context)
        {
            _context = context;
        }

        public string DatabaseName { get; private set; }

        public bool CheckDatabaseExist(string provider, string connectionString)
        {
            Log.Debug($"checking if database is setup...");

            bool exists = false;

            try
            {
                var con = GetConnection(provider, connectionString);

                exists = _context.Database.Exists();
                if (exists)
                {
                    DatabaseName = _context.Database.Connection.Database;
                    Log.Debug($"using database:{DatabaseName}");
                }
                else
                {
                    Log.Debug($"No database FOUND!");
                }

            }
            catch (Exception e)
            {
                Log.Debug(e);
            }

            return exists;
        }

        public Task RunUpdateAsync(IProgress<DProgress> progress = null)
        {
            progress?.ReportStatus("checking database...");
            return Task.Run(() =>
            {
                var configuration = new Configuration();
                var migrator = new DbMigrator(configuration);
                migrator.Update();
            });
        }
        public IDbConnection GetConnection(string provider, string connectionString)
        {
            DbConnection connection = null;
            var providerName = provider;

            if (providerName.ToLower().Contains("System.Data.SqlClient".ToLower()))
            {
                connection = new SqlConnection(connectionString);
            }
            if (providerName.ToLower().Contains("MySql.Data.MySqlClient".ToLower()))
            {
                connection = new MySqlConnection(connectionString);
                
            }
            if (providerName.ToLower().Contains("Npgsql".ToLower()))
            {
                connection = new NpgsqlConnection(connectionString);
                
            }
            return connection;
        }

        public DatabaseConfig GetDatabaseConfig(string provider, string connectionString)
        {
            DatabaseConfig databaseConfig=new DatabaseConfig();

            var providerName = provider;

            if (providerName.ToLower().Contains("System.Data.SqlClient".ToLower()))
            {
                //  connectionString="Data Source=.\SQLExpress;Initial Catalog=DWAPIRemote;Persist Security Info=True;User ID=sa;Password=c0nstella;MultipleActiveResultSets=True;Pooling=True"

                var sqlBuilder =new SqlConnectionStringBuilder(connectionString);
                databaseConfig.Server = sqlBuilder.DataSource;
                databaseConfig.Database = sqlBuilder.InitialCatalog;
                databaseConfig.User = sqlBuilder.UserID;
                databaseConfig.Password = sqlBuilder.Password;

            }
            if (providerName.ToLower().Contains("MySql.Data.MySqlClient".ToLower()))
            {
                //  connectionString="Server=127.0.0.1;Port=3306;Database=hmis;Uid=root;Pwd=;convert zero datetime=True"

                var mySqlConnectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);
                databaseConfig.Server = mySqlConnectionStringBuilder.Server;
                databaseConfig.Port = (int) mySqlConnectionStringBuilder.Port;
                databaseConfig.Database = mySqlConnectionStringBuilder.Database;
                databaseConfig.User = mySqlConnectionStringBuilder.UserID;
                databaseConfig.Password = mySqlConnectionStringBuilder.Password;
            }
            if (providerName.ToLower().Contains("Npgsql".ToLower()))
            {
                //  connectionString="Server=127.0.0.1;Port=5432;Database=hmis;User Id=postgres;Password=postgres;"

                var npgsqlConnectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString);
                databaseConfig.Server = npgsqlConnectionStringBuilder.Host;
                databaseConfig.Port = npgsqlConnectionStringBuilder.Port;
                databaseConfig.Database = npgsqlConnectionStringBuilder.Database;
                databaseConfig.User = npgsqlConnectionStringBuilder.Username;
                databaseConfig.Password = npgsqlConnectionStringBuilder.Password;
            }

            return databaseConfig;
        }
        

        public  async Task<bool> CheckConnection(DatabaseConfig databaseConfig)
        {
            bool connectionOk = false;
            var con = GetConnection(databaseConfig.DatabaseType.Provider, databaseConfig.GetConnectionString());
            if (con != null)
            {
                try
                {
                    connectionOk = await Task.Run(() =>
                        {
                            con.Open();
                            return con.State == ConnectionState.Open;
                        }
                    );
                }
                catch (Exception e)
                {
                    Log.Debug(e);
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
            return connectionOk;

        }

        public async Task<List<string>> GetSqlServersList(IProgress<DProgress> progress = null)
        {
            progress?.ReportStatus("Searching...");

            List<string> listOfServers = new List<string>();
            DataTable sqlServersTable;
            try
            {
                sqlServersTable = await Task.Run(() => SqlDataSourceEnumerator.Instance.GetDataSources());
            }
            catch (Exception e)
            {
                Log.Debug(e);
                throw;
            }
            

            foreach (DataRow rowOfData in sqlServersTable.Rows)
            {
                //get the server name
                string serverName = rowOfData["ServerName"].ToString();
                //get the instance name
                string instanceName = rowOfData["InstanceName"].ToString();
                serverName = $"{serverName}{(string.IsNullOrWhiteSpace(instanceName)?string.Empty: $@"\{instanceName}")}";
                listOfServers.Add(serverName);
            }
            if (listOfServers.Count > 0)
                listOfServers.Sort();

            progress?.ReportStatus($"Search found {listOfServers.Count}");

            return listOfServers;
        }

        public async Task<List<string>> GetDatabaseList(DatabaseConfig databaseConfig, IProgress<DProgress> progress = null)
        {
            progress?.ReportStatus("Loading databases...");

            List<string> databaseList = new List<string>();

            string providerName = databaseConfig.DatabaseType.Provider;
            string connectionString = databaseConfig.GetConnectionString();

            if (providerName.ToLower().Contains("System.Data.SqlClient".ToLower()))
            {
                databaseConfig.Database = "master";
                connectionString = databaseConfig.GetConnectionString();

                string sql = @"
                                SELECT	name
                                FROM	sys.databases
                                WHERE	(name LIKE '%IQTools%')
                            ";
                using (var connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                       await connection.OpenAsync();
                    }
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (dr.Read())
                        {
                            var dbname = dr[0].ToString();
                            databaseList.Add(dbname);
                        }
                    }
                }
            }

            if (providerName.ToLower().Contains("MySql.Data.MySqlClient".ToLower()))
            {
                string sql = @"SHOW DATABASES;";

                using (var connection = new MySqlConnection(connectionString))
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        await connection.OpenAsync();
                    }
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var dbname = dr[0].ToString();
                            databaseList.Add(dbname);
                        }
                    }
                }

            }

            //            
            if (providerName.ToLower().Contains("Npgsql".ToLower()))
            {
                string sql = @"SELECT datname FROM pg_database WHERE datistemplate = false;";

                using (var connection = new NpgsqlConnection(connectionString))
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        await connection.OpenAsync();
                    }
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var dbname = dr[0].ToString();
                            databaseList.Add(dbname);
                        }
                    }
                }

            }

            if (databaseList.Count > 0)
                databaseList.Sort();

            progress?.ReportStatus($"Databases found {databaseList.Count}");

            return databaseList;
        }
    }
}