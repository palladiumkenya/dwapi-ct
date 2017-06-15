namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventHistoryRev003 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventHistory", "Imported", c => c.Int());
            AddColumn("dbo.EventHistory", "NotImported", c => c.Int());
            AddColumn("dbo.EventHistory", "ImportDate", c => c.DateTime());
            AddColumn("dbo.EventHistory", "ImportStatus", c => c.String(maxLength: 150));
            AddColumn("dbo.EventHistory", "IsImportSuccess", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventHistory", "IsImportSuccess");
            DropColumn("dbo.EventHistory", "ImportStatus");
            DropColumn("dbo.EventHistory", "ImportDate");
            DropColumn("dbo.EventHistory", "NotImported");
            DropColumn("dbo.EventHistory", "Imported");
        }
    }
}
