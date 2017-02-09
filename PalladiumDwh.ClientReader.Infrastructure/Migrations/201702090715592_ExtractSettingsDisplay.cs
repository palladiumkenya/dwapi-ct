namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtractSettingsDisplay : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExtractSetting", "Display", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExtractSetting", "Display");
        }
    }
}
