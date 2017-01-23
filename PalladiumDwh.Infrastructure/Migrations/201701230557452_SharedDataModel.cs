namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SharedDataModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Facility", "Emr", c => c.String(maxLength: 150));
            AddColumn("dbo.Facility", "Project", c => c.String(maxLength: 150));
            AddColumn("dbo.Facility", "Voided", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Facility", "Voided");
            DropColumn("dbo.Facility", "Project");
            DropColumn("dbo.Facility", "Emr");
        }
    }
}
