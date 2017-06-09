using System;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces;
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
            var exists = _databaseManager.CheckDatabaseExist();
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

        private void ReportDProgress(DProgress value)
        {
            Console.WriteLine(value);
        }
    }
}