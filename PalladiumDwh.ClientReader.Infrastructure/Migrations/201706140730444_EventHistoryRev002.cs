namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventHistoryRev002 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EventHistory", "IsFoundSuccess", c => c.Boolean());
            AlterColumn("dbo.EventHistory", "IsLoadSuccess", c => c.Boolean());
            AlterColumn("dbo.EventHistory", "IsSendSuccess", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EventHistory", "IsSendSuccess", c => c.Boolean(nullable: false));
            AlterColumn("dbo.EventHistory", "IsLoadSuccess", c => c.Boolean(nullable: false));
            AlterColumn("dbo.EventHistory", "IsFoundSuccess", c => c.Boolean(nullable: false));
        }
    }
}
