using EntityFramework.Seeder;

namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration :
        DbMigrationsConfiguration<PalladiumDwh.ClientReader.Infrastructure.Data.DwapiRemoteContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PalladiumDwh.ClientReader.Infrastructure.Data.DwapiRemoteContext context)
        {
   

            Seeder.Configuration.Delimiter = "|";
            Seeder.Configuration.TrimFields = true;
            Seeder.Configuration.TrimHeaders = true;
            



            context.Projects.SeedFromResource("PalladiumDwh.ClientReader.Infrastructure.Seed.Project.csv", c => c.Code);
            context.SaveChanges();
            context.Emrs.SeedFromResource("PalladiumDwh.ClientReader.Infrastructure.Seed.EMR.csv", c =>new {c.Name,c.Version,c.ProjectId});
            context.SaveChanges();
            context.ExtractSettings.SeedFromResource("PalladiumDwh.ClientReader.Infrastructure.Seed.ExtractSetting.csv", c => new {c.Name,c.EmrId});
            context.SaveChanges();
         
        }
    }
}
