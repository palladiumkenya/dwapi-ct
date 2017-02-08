namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmrSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EMR", "ConnectionKey", c => c.String(maxLength: 150));
            AddColumn("dbo.ExtractSetting", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExtractSetting", "IsActive");
            DropColumn("dbo.EMR", "ConnectionKey");
        }
    }
}
