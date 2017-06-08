using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.ClientReader.Core.Services;
using PalladiumDwh.Shared.Custom;

namespace PalladiumDwh.ClientReader.Core.Tests.Services
{
    [TestFixture]
    public class DatabaseSetupServiceTests
    {
        private readonly string _localKey = "DWAPIRemote";
        private IDatabaseSetupService _service;
        private string _appDir;
        private string _dbconfig;
        private string _dbconfigBackup;

        [SetUp]
        public void SetUp()
        {
            _appDir = $@"{Utility.GetFolderPath(TestContext.CurrentContext.TestDirectory).HasToEndsWith(@"\")}";
            _dbconfig = $@"{_appDir.HasToEndsWith(@"\")}database.config";
            _dbconfigBackup = $@"{_appDir.HasToEndsWith(@"\")}database.config.bak";
            _service =new DatabaseSetupService(_dbconfig);
        }

        [Test]
        public void void_should_GetAll_Connections()
        {
            var conSettings = _service.ConnectionStringSettingses;
            Assert.IsNotEmpty(conSettings);

            foreach (var cn in conSettings)
            {
                Console.WriteLine($"{cn.Name} ({cn.ProviderName})");
                Console.WriteLine(new string('-',40));
                Console.WriteLine($">. {cn.ConnectionString}");
            }
        }

        [Test]
        public void void_should_SaveChanges_App_Connection()
        {
            var emrdbtype = DatabaseType.GetAll().First(x => x.Provider.ToLower() == "System.Data.SqlClient".ToLower());
            Assert.IsNotNull(emrdbtype);

            var dbConfig = new DatabaseConfig();
            dbConfig.DatabaseType = emrdbtype;
            dbConfig.Server = @"SERVER\X";
            dbConfig.Password = "XUserx";
            dbConfig.Port = 100;
            dbConfig.User = "XUserx";

            _service.Save(dbConfig);
            _service.Refresh();

            var connectionStringSettings = _service.ConnectionStringSettingses.First(x => x.Name.ToLower() == _localKey.ToLower());
            Assert.IsNotNull(connectionStringSettings);
            Assert.IsTrue(connectionStringSettings.ConnectionString.Contains(@"SERVER\X"));
            Console.WriteLine($"{connectionStringSettings.Name} ({connectionStringSettings.ProviderName})");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine($">. {connectionStringSettings.ConnectionString}");
        }

        [Test]
        [TestCase(@"EMRDatabase")]
        [TestCase(@"MySQLEMRDatabase")]
        [TestCase(@"PostgreSQLEMRDatabase")]
        public void void_should_SaveChanges_EmrApp_Connection(string key)
        {
            var emrdbtype = DatabaseType.GetAll().First(x => x.Key.ToLower() == key.ToLower());
            Assert.IsNotNull(emrdbtype);

            var dbConfig = new DatabaseConfig();
            dbConfig.DatabaseType = emrdbtype;
            dbConfig.Server = @"127.0.0.1";
            dbConfig.Database = "emrdatabase";
            dbConfig.Password = "emrpassword";
            dbConfig.User = "emruser";

            _service.SaveEmr(dbConfig);
            _service.Refresh();

            var connectionStringSettings = _service.ConnectionStringSettingses.First(x => x.Name.ToLower() == emrdbtype.Key.ToLower());
            Assert.IsNotNull(connectionStringSettings);
            Assert.IsTrue(connectionStringSettings.ConnectionString.Contains(dbConfig.Server));
            Console.WriteLine($"{connectionStringSettings.Name} ({connectionStringSettings.ProviderName})");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine($">. {connectionStringSettings.ConnectionString}");
        }

        [TearDown]
        public void TearDown()
        {
            File.Copy(_dbconfigBackup, _dbconfig, true);
        }
    }
}