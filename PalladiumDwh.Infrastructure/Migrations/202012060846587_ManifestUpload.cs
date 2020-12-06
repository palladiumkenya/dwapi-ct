namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManifestUpload : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FacilityManifest", "UploadMode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FacilityManifest", "UploadMode");
        }
    }
}
