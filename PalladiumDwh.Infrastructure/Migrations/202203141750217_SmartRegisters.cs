namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SmartRegisters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SmartActionRegister",
                c => new
                    {
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        Id = c.Guid(nullable: false),
                        Action = c.String(maxLength: 150),
                        Area = c.String(maxLength: 150),
                        FacilityId = c.Guid(nullable: false),
                        ManifestId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SmartActionRegister");
        }
    }
}
