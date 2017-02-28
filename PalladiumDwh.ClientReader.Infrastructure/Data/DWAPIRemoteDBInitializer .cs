using System.Data.Entity;
using EntityFramework.Seeder;
using PalladiumDwh.ClientReader.Infrastructure.Migrations;

namespace PalladiumDwh.ClientReader.Infrastructure.Data
{
    public class DwapiRemoteDbInitializer : CreateDatabaseIfNotExists<DwapiRemoteContext>
    {
        protected override void Seed(DwapiRemoteContext context)
        {
            LiveSeeder.Plant(context);
        }
    }
}