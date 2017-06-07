using System;
using NUnit.Framework;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Infrastructure.Data;

namespace PalladiumDwh.ClientReader.Infrastructure.Tests.Data
{
    [TestFixture]
    public class DatabaseManagerTests
    {
        private DwapiRemoteContext _context;
        private IDatabaseManager _databaseManager;

        [SetUp]
        public void SetUp()
        {
            _context = new DwapiRemoteContext();
            _databaseManager=new DatabaseManager(_context);
        }

        [Test]
        public void should_CheckDatabaseExist()
        {
            var exists = _databaseManager.CheckDatabaseExist();
            Assert.True(exists);
            Assert.IsFalse(string.IsNullOrWhiteSpace(_databaseManager.DatabaseName));
            Console.WriteLine($"Database:{_databaseManager.DatabaseName}");
        }
    }
}