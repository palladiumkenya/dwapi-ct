namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdverseEvent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientAdverseEventExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        Id = c.Guid(nullable: false),
                        AdverseEvent = c.String(maxLength: 150),
                        AdverseEventStartDate = c.DateTime(),
                        AdverseEventEndDate = c.DateTime(),
                        Severity = c.String(maxLength: 150),
                        AdverseEventClinicalOutcome = c.String(maxLength: 150),
                        AdverseEventActionTaken = c.String(maxLength: 150),
                        AdverseEventIsPregnant = c.Boolean(),
                        VisitDate = c.DateTime(),
                        PatientId = c.Guid(nullable: false),
                        Created = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientAdverseEventExtract", "PatientId", "dbo.PatientExtract");
            DropIndex("dbo.PatientAdverseEventExtract", new[] { "PatientId" });
            DropTable("dbo.PatientAdverseEventExtract");
        }
    }
}
