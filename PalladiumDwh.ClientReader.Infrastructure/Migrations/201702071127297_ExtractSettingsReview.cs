namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtractSettingsReview : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ExtractSetting", "ExtractSql", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ExtractSetting", "ExtractSql", c => c.String());
        }
    }
}
