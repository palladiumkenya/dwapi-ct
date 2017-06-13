namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventHistoryRev001 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EventHistory", "SiteCode", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EventHistory", "SiteCode", c => c.Int(nullable: false));
        }
    }
}
