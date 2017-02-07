namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtractsSetting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EMR",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 150),
                        Version = c.String(maxLength: 150),
                        IsDefault = c.Boolean(nullable: false),
                        ProjectId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Project", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.ExtractSetting",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 150),
                        ExtractCsv = c.String(maxLength: 150),
                        ExtractSql = c.String(),
                        EmrId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EMR", t => t.EmrId, cascadeDelete: true)
                .Index(t => t.EmrId);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(maxLength: 150),
                        Name = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EMR", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.ExtractSetting", "EmrId", "dbo.EMR");
            DropIndex("dbo.ExtractSetting", new[] { "EmrId" });
            DropIndex("dbo.EMR", new[] { "ProjectId" });
            DropTable("dbo.Project");
            DropTable("dbo.ExtractSetting");
            DropTable("dbo.EMR");
        }
    }
}
