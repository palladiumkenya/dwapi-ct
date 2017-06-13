namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialEventHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventHistory",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        Display = c.String(maxLength: 150),
                        Found = c.Int(),
                        FoundDate = c.DateTime(),
                        FoundStatus = c.String(maxLength: 150),
                        IsFoundSuccess = c.Boolean(nullable: false),
                        Loaded = c.Int(),
                        Rejected = c.Int(),
                        LoadDate = c.DateTime(),
                        LoadStatus = c.String(maxLength: 150),
                        IsLoadSuccess = c.Boolean(nullable: false),
                        Sent = c.Int(),
                        NotSent = c.Int(),
                        SendDate = c.DateTime(),
                        SendStatus = c.String(maxLength: 150),
                        IsSendSuccess = c.Boolean(nullable: false),
                        ExtractSettingId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExtractSetting", t => t.ExtractSettingId, cascadeDelete: true)
                .Index(t => t.ExtractSettingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventHistory", "ExtractSettingId", "dbo.ExtractSetting");
            DropIndex("dbo.EventHistory", new[] { "ExtractSettingId" });
            DropTable("dbo.EventHistory");
        }
    }
}
