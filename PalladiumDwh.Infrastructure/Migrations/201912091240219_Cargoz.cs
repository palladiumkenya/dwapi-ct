namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cargoz : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FacilityManifestCargo", "CargoType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FacilityManifestCargo", "CargoType");
        }
    }
}
