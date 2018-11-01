namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Manfiesta : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FacilityManifestCargo",
                c => new
                    {
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        Id = c.Guid(nullable: false),
                        Items = c.String(maxLength: 150),
                        FacilityManifestId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FacilityManifest", t => t.FacilityManifestId, cascadeDelete: true)
                .Index(t => t.FacilityManifestId);
            
            CreateTable(
                "dbo.FacilityManifest",
                c => new
                    {
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        Id = c.Guid(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PatientCount = c.Int(nullable: false),
                        DateRecieved = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FacilityManifestCargo", "FacilityManifestId", "dbo.FacilityManifest");
            DropIndex("dbo.FacilityManifestCargo", new[] { "FacilityManifestId" });
            DropTable("dbo.FacilityManifest");
            DropTable("dbo.FacilityManifestCargo");
        }
    }
}
