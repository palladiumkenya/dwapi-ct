namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmrDestination : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExtractSetting", "Destination", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExtractSetting", "Destination");
        }
    }
}
