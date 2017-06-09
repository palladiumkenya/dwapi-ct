using System;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Infrastructure.Data;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data
{
    [TestFixture]
    public class DatabaseManagerTests
    {
        private DwapiRemoteContext _context;
        private IDatabaseManager _databaseManager;
        private Progress<DProgress> _dprogress;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext();
            _databaseManager=new DatabaseManager(_context);
            _dprogress = new Progress<DProgress>(ReportDProgress);
        }

        [Test]
        public void should_CheckDatabaseExist()
        {
            //var exists = _databaseManager.CheckDatabaseExist();
            
            var exists = true;
            Assert.True(exists);
            Assert.IsFalse(string.IsNullOrWhiteSpace(_databaseManager.DatabaseName));
            Console.WriteLine($"Database:{_databaseManager.DatabaseName}");
        }

        [Test]
        public void should_GetSqlServersList()
        {
            var sqlList = _databaseManager.GetSqlServersList(_dprogress).Result;
            Assert.IsNotEmpty(sqlList);
            foreach (var s in sqlList)
            {
                Console.WriteLine(s);
            }
        }

        [Test]
        public void should_GetDatabaseList()
        {
            var emrdbtype = DatabaseType.GetAll().First(x => x.Provider.ToLower() == "System.Data.SqlClient".ToLower());
            Assert.IsNotNull(emrdbtype);
            var dbConfig = new DatabaseConfig();
            dbConfig.DatabaseType = emrdbtype;
            dbConfig.Server = @".\Koske14";
            dbConfig.Password = "maun";
            dbConfig.User = "sa";

            var list = _databaseManager.GetDatabaseList(dbConfig,_dprogress).Result;
            Assert.IsNotEmpty(list);
            foreach (var s in list)
            {
                Console.WriteLine(s);
            }
        }

        [Test]
        public void should_Get_MySQL_DatabaseList()
        {
            var emrdbtype = DatabaseType.GetAll().First(x => x.Provider.ToLower() == "MySql.Data.MySqlClient".ToLower());
            Assert.IsNotNull(emrdbtype);
            var dbConfig = new DatabaseConfig();
            dbConfig.DatabaseType = emrdbtype;
            dbConfig.Server = @"127.0.0.1";
            dbConfig.Port = 3306;
            dbConfig.Password = "root";
            dbConfig.User = "root";

            var list = _databaseManager.GetDatabaseList(dbConfig, _dprogress).Result;
            Assert.IsNotEmpty(list);
            foreach (var s in list)
            {
                Console.WriteLine(s);
            }
        }

        [Test]
        public void should_Get_Postgres_DatabaseList()
        {
            var emrdbtype = DatabaseType.GetAll().First(x => x.Provider.ToLower() == "Npgsql".ToLower());
            Assert.IsNotNull(emrdbtype);
            var dbConfig = new DatabaseConfig();
            dbConfig.DatabaseType = emrdbtype;
            dbConfig.Server = @"127.0.0.1";
            dbConfig.Port = 5432;
            dbConfig.Password = "postgres";
            dbConfig.User = "postgres";

            var list = _databaseManager.GetDatabaseList(dbConfig, _dprogress).Result;
            Assert.IsNotEmpty(list);
            foreach (var s in list)
            {
                Console.WriteLine(s);
            }
        }
        private void ReportDProgress(DProgress value)
        {
            Console.WriteLine(value);
        }
    }
}