namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IITRiskScores : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IITRiskScoresExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        PatientId = c.Guid(nullable: false),
                        SourceSysUUID = c.String(maxLength: 150),
                        RiskScore = c.Decimal(precision: 18, scale: 2),
                        RiskFactors = c.String(maxLength: 150),
                        RiskDescription = c.String(maxLength: 150),
                        RiskEvaluationDate = c.DateTime(),
                        Created = c.DateTime(),
                        Date_Created = c.DateTime(),
                        Date_Last_Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.StageIITRiskScoresExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        SourceSysUUID = c.String(maxLength: 150),
                        RiskScore = c.Decimal(precision: 18, scale: 2),
                        RiskFactors = c.String(maxLength: 150),
                        RiskDescription = c.String(maxLength: 150),
                        RiskEvaluationDate = c.DateTime(),
                        Date_Created = c.DateTime(),
                        Date_Last_Modified = c.DateTime(),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        SiteCode = c.Int(nullable: false),
                        PatientPK = c.Int(nullable: false),
                        FacilityId = c.Guid(),
                        CurrentPatientId = c.Guid(),
                        LiveSession = c.Guid(),
                        LiveStage = c.Int(nullable: false),
                        Generated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IITRiskScoresExtract", "PatientId", "dbo.PatientExtract");
            DropIndex("dbo.IITRiskScoresExtract", new[] { "PatientId" });
            DropTable("dbo.StageIITRiskScoresExtract");
            DropTable("dbo.IITRiskScoresExtract");
        }
    }
}
