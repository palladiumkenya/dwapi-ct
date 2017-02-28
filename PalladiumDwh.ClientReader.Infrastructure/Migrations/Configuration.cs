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

            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator()); //it will generate MySql commands instead of SqlServer commands.

            SetHistoryContextFactory("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema)); //here s the thing.


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
