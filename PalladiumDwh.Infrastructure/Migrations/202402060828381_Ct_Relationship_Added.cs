namespace PalladiumDwh.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ct_Relationship_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RelationshipsExtract",
                c => new
                    {
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Processed = c.Boolean(nullable: false),
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        RelationshipToPatient = c.String(maxLength: 150),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        RecordUUID = c.String(maxLength: 150),
                        Date_Created = c.DateTime(),
                        Date_Last_Modified = c.DateTime(),
                        Created = c.DateTime(),
                        PatientId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PatientExtract", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.StageRelationshipsExtract",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FacilityName = c.String(maxLength: 150),
                        RelationshipToPatient = c.String(maxLength: 150),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        RecordUUID = c.String(maxLength: 150),
                        Voided = c.Boolean(nullable: false),
                        Date_Created = c.DateTime(),
                        Date_Last_Modified = c.DateTime(),
                        Emr = c.String(maxLength: 150),
                        Project = c.String(maxLength: 150),
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
            
            AddColumn("dbo.PatientVisitExtract", "WHOStagingOI", c => c.String(maxLength: 150));
            AddColumn("dbo.StageVisitExtract", "WHOStagingOI", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RelationshipsExtract", "PatientId", "dbo.PatientExtract");
            DropIndex("dbo.RelationshipsExtract", new[] { "PatientId" });
            DropColumn("dbo.StageVisitExtract", "WHOStagingOI");
            DropColumn("dbo.PatientVisitExtract", "WHOStagingOI");
            DropTable("dbo.StageRelationshipsExtract");
            DropTable("dbo.RelationshipsExtract");
        }
    }
}
