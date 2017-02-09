namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtractSettingsRank : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExtractSetting", "Rank", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExtractSetting", "Rank");
        }
    }
}
