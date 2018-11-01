namespace PalladiumDwh.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<PalladiumDwh.Infrastructure.Data.DwapiCentralContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PalladiumDwh.Infrastructure.Data.DwapiCentralContext context)
        {
          //  LiveSeeder.Plant(context);
        }
    }
}
