using System;
using System.Data.Entity.Migrations;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using PalladiumDwh.ClientReader.Core.Interfaces;
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

        public bool CheckDatabaseExist()
        {
            Log.Debug($"checking if database is setup...");

            bool exists = false;

            try
            {
                exists= _context.Database.Exists();
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

        public  Task RunUpdateAsync(IProgress<DProgress> progress = null)
        {
            progress?.ReportStatus("checking database...");
            return Task.Run(() =>
            {
                var configuration = new Configuration();
                var migrator = new DbMigrator(configuration);
                migrator.Update();
            });
        }
    }
}
