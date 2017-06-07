using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PalladiumDwh.Shared.Data.EFConvention;

namespace PalladiumDwh.Shared.Data
{
    public abstract class DwapiBaseContext : DbContext
    {
        protected DwapiBaseContext(string connection) : base(connection)
        {
        }

        protected DwapiBaseContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
        }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(150));
            modelBuilder.Conventions.AddBefore<ForeignKeyIndexConvention>(new ForeignKeyNamingConvention());
        }
    }
}
