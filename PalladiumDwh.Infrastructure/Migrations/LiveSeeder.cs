using EntityFramework.Seeder;
using PalladiumDwh.Infrastructure.Data;

namespace PalladiumDwh.Infrastructure.Migrations
{
    public class LiveSeeder
    {
        public static void Plant(DwapiCentralContext context)
        {
            Seeder.Configuration.Delimiter = "|";
            Seeder.Configuration.TrimFields = true;
            Seeder.Configuration.TrimHeaders = true;

            context.MasterFacilities.SeedFromResource("PalladiumDwh.Infrastructure.Seed.MasterFacility.csv", c => c.Code);
            context.SaveChanges();
        }
    }
}