using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.Shared.Custom;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    public class UpdateDatabase : IUpdateDatabase
    {
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
