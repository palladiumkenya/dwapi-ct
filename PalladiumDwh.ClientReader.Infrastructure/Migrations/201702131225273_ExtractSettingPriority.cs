namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtractSettingPriority : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExtractSetting", "IsPriority", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExtractSetting", "IsPriority");
        }
    }
}
