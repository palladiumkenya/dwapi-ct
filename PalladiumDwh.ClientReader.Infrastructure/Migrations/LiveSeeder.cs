using System;
using System.Data.Entity;
using EntityFramework.Seeder;
using PalladiumDwh.ClientReader.Infrastructure.Data;

namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    public class LiveSeeder
    {
        public static void Plant(DwapiRemoteContext context)
        {
            Seeder.Configuration.Delimiter = "|";
            Seeder.Configuration.TrimFields = true;
            Seeder.Configuration.TrimHeaders = true;

            context.Projects.SeedFromResource("PalladiumDwh.ClientReader.Infrastructure.Seed.Project.csv", c => c.Code);
            context.SaveChanges();
            context.Emrs.SeedFromResource("PalladiumDwh.ClientReader.Infrastructure.Seed.EMR.csv", c => new { c.Name, c.Version, c.ProjectId });
            context.SaveChanges();
            context.ExtractSettings.SeedFromResource("PalladiumDwh.ClientReader.Infrastructure.Seed.ExtractSetting.csv", c => new { c.Name, c.EmrId });
            context.SaveChanges();
        }
    }
}