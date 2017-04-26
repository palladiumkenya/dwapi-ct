namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allowNullsInTemp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TempPatientArtExtract", "PatientPK", c => c.Int());
            AlterColumn("dbo.TempPatientArtExtract", "SiteCode", c => c.Int());
            AlterColumn("dbo.TempPatientBaselinesExtract", "PatientPK", c => c.Int());
            AlterColumn("dbo.TempPatientBaselinesExtract", "SiteCode", c => c.Int());
            AlterColumn("dbo.TempPatientExtract", "PatientPK", c => c.Int());
            AlterColumn("dbo.TempPatientExtract", "SiteCode", c => c.Int());
            AlterColumn("dbo.TempPatientLaboratoryExtract", "PatientPK", c => c.Int());
            AlterColumn("dbo.TempPatientLaboratoryExtract", "SiteCode", c => c.Int());
            AlterColumn("dbo.TempPatientPharmacyExtract", "PatientPK", c => c.Int());
            AlterColumn("dbo.TempPatientPharmacyExtract", "SiteCode", c => c.Int());
            AlterColumn("dbo.TempPatientStatusExtract", "PatientPK", c => c.Int());
            AlterColumn("dbo.TempPatientStatusExtract", "SiteCode", c => c.Int());
            AlterColumn("dbo.TempPatientVisitExtract", "PatientPK", c => c.Int());
            AlterColumn("dbo.TempPatientVisitExtract", "SiteCode", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TempPatientVisitExtract", "SiteCode", c => c.Int(nullable: false));
            AlterColumn("dbo.TempPatientVisitExtract", "PatientPK", c => c.Int(nullable: false));
            AlterColumn("dbo.TempPatientStatusExtract", "SiteCode", c => c.Int(nullable: false));
            AlterColumn("dbo.TempPatientStatusExtract", "PatientPK", c => c.Int(nullable: false));
            AlterColumn("dbo.TempPatientPharmacyExtract", "SiteCode", c => c.Int(nullable: false));
            AlterColumn("dbo.TempPatientPharmacyExtract", "PatientPK", c => c.Int(nullable: false));
            AlterColumn("dbo.TempPatientLaboratoryExtract", "SiteCode", c => c.Int(nullable: false));
            AlterColumn("dbo.TempPatientLaboratoryExtract", "PatientPK", c => c.Int(nullable: false));
            AlterColumn("dbo.TempPatientExtract", "SiteCode", c => c.Int(nullable: false));
            AlterColumn("dbo.TempPatientExtract", "PatientPK", c => c.Int(nullable: false));
            AlterColumn("dbo.TempPatientBaselinesExtract", "SiteCode", c => c.Int(nullable: false));
            AlterColumn("dbo.TempPatientBaselinesExtract", "PatientPK", c => c.Int(nullable: false));
            AlterColumn("dbo.TempPatientArtExtract", "SiteCode", c => c.Int(nullable: false));
            AlterColumn("dbo.TempPatientArtExtract", "PatientPK", c => c.Int(nullable: false));
        }
    }
}
