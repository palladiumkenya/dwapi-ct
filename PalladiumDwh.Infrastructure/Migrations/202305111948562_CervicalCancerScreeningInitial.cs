namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CervicalCancerScreeningInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CervicalCancerScreeningExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        VisitID = c.Int(),
                        VisitDate = c.DateTime(),
                        VisitType = c.String(maxLength: 150),
                        ScreeningMethod = c.String(maxLength: 150),
                        TreatmentToday = c.String(maxLength: 150),
                        ReferredOut = c.String(maxLength: 150),
                        NextAppointmentDate = c.DateTime(),
                        ScreeningType = c.String(maxLength: 150),
                        ScreeningResult = c.String(maxLength: 150),
                        PostTreatmentComplicationCause = c.String(maxLength: 150),
                        OtherPostTreatmentComplication = c.String(maxLength: 150),
                        ReferralReason = c.String(maxLength: 150),
                        Created = c.DateTime(),
                        Date_Created = c.DateTime(),
                        Date_Last_Modified = c.DateTime(),
                        PatientId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.StageCervicalCancerScreeningExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        VisitID = c.Int(),
                        VisitDate = c.DateTime(),
                        VisitType = c.String(maxLength: 150),
                        ScreeningMethod = c.String(maxLength: 150),
                        TreatmentToday = c.String(maxLength: 150),
                        ReferredOut = c.String(maxLength: 150),
                        NextAppointmentDate = c.DateTime(),
                        ScreeningType = c.String(maxLength: 150),
                        ScreeningResult = c.String(maxLength: 150),
                        PostTreatmentComplicationCause = c.String(maxLength: 150),
                        OtherPostTreatmentComplication = c.String(maxLength: 150),
                        ReferralReason = c.String(maxLength: 150),
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
            DropForeignKey("dbo.CervicalCancerScreeningExtract", "PatientId", "dbo.PatientExtract");
            DropIndex("dbo.CervicalCancerScreeningExtract", new[] { "PatientId" });
            DropTable("dbo.StageCervicalCancerScreeningExtract");
            DropTable("dbo.CervicalCancerScreeningExtract");
        }
    }
}
