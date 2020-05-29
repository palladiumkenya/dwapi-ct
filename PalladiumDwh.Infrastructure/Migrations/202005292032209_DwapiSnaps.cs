namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DwapiSnaps : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Facility", "SnapshotDate", c => c.DateTime());
            AddColumn("dbo.Facility", "SnapshotSiteCode", c => c.Int());
            AddColumn("dbo.Facility", "SnapshotVersion", c => c.Int());
            AddColumn("dbo.FacilityManifest", "Name", c => c.String(maxLength: 150));
            AddColumn("dbo.FacilityManifest", "EmrId", c => c.Guid());
            AddColumn("dbo.FacilityManifest", "EmrName", c => c.String(maxLength: 150));
            AddColumn("dbo.FacilityManifest", "EmrSetup", c => c.Int(nullable: false));
            AddColumn("dbo.MasterFacility", "SnapshotDate", c => c.DateTime());
            AddColumn("dbo.MasterFacility", "SnapshotSiteCode", c => c.Int());
            AddColumn("dbo.MasterFacility", "SnapshotVersion", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MasterFacility", "SnapshotVersion");
            DropColumn("dbo.MasterFacility", "SnapshotSiteCode");
            DropColumn("dbo.MasterFacility", "SnapshotDate");
            DropColumn("dbo.FacilityManifest", "EmrSetup");
            DropColumn("dbo.FacilityManifest", "EmrName");
            DropColumn("dbo.FacilityManifest", "EmrId");
            DropColumn("dbo.FacilityManifest", "Name");
            DropColumn("dbo.Facility", "SnapshotVersion");
            DropColumn("dbo.Facility", "SnapshotSiteCode");
            DropColumn("dbo.Facility", "SnapshotDate");
        }
    }
}
