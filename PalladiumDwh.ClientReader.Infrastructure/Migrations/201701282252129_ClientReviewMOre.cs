namespace PalladiumDwh.ClientReader.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientReviewMOre : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PatientArtExtract", "Processed", c => c.Boolean());
            AlterColumn("dbo.PatientBaselinesExtract", "Processed", c => c.Boolean());
            AlterColumn("dbo.PatientExtract", "Processed", c => c.Boolean());
            AlterColumn("dbo.PatientLaboratoryExtract", "Processed", c => c.Boolean());
            AlterColumn("dbo.PatientPharmacyExtract", "Processed", c => c.Boolean());
            AlterColumn("dbo.PatientStatusExtract", "Processed", c => c.Boolean());
            AlterColumn("dbo.PatientVisitExtract", "Processed", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PatientVisitExtract", "Processed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PatientStatusExtract", "Processed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PatientPharmacyExtract", "Processed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PatientLaboratoryExtract", "Processed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PatientExtract", "Processed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PatientBaselinesExtract", "Processed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PatientArtExtract", "Processed", c => c.Boolean(nullable: false));
        }
    }
}
