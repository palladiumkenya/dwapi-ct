namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MFLReview001 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MasterFacility", "FacilityId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MasterFacility", "FacilityId");
        }
    }
}
