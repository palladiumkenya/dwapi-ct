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
            LiveSeeder.Plant(context);
        }
    }
}
