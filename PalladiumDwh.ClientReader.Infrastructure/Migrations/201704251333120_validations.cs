namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ValidationError",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ValidatorId = c.Guid(nullable: false),
                        RecordId = c.Guid(nullable: false),
                        DateGenerated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Validator", t => t.ValidatorId, cascadeDelete: true)
                .Index(t => t.ValidatorId);
            
            CreateTable(
                "dbo.Validator",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Extract = c.String(maxLength: 150),
                        Field = c.String(maxLength: 150),
                        Type = c.String(maxLength: 150),
                        Logic = c.String(maxLength: 150),
                        Summary = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ValidationError", "ValidatorId", "dbo.Validator");
            DropIndex("dbo.ValidationError", new[] { "ValidatorId" });
            DropTable("dbo.Validator");
            DropTable("dbo.ValidationError");
        }
    }
}
