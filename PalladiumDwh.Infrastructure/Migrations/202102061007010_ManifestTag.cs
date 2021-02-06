namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManifestTag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FacilityManifest", "Session", c => c.Guid());
            AddColumn("dbo.FacilityManifest", "Start", c => c.DateTime());
            AddColumn("dbo.FacilityManifest", "End", c => c.DateTime());
            AddColumn("dbo.FacilityManifest", "Tag", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FacilityManifest", "Tag");
            DropColumn("dbo.FacilityManifest", "End");
            DropColumn("dbo.FacilityManifest", "Start");
            DropColumn("dbo.FacilityManifest", "Session");
        }
    }
}
