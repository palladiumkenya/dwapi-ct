using System.Data.Entity;
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